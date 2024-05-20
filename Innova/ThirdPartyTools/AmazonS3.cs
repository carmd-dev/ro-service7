using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Innova.ThirdPartyTools
{
    /// <summary>
    /// The AmazonS3 class is a wrapper for the .NET library supplied by Amazon.
    /// </summary>
    public class AmazonS3
    {
        private string awsAccessKey;
        private string awsSecretKey;
        private List<AmazonS3Error> amazonS3Errors;

        /// <summary>
        /// The public contructor for the AmazonS3 class.
        /// </summary>
        /// <param name="awsAccessKey">The <see cref="string"/> Amazon Web Service access key.</param>
        /// <param name="awsSecretKey">The <see cref="string"/> Amazon Web Service secret key.</param>
        public AmazonS3(string awsAccessKey, string awsSecretKey)
        {
            this.awsAccessKey = awsAccessKey;
            this.awsSecretKey = awsSecretKey;

            this.SetAmazonS3Errors();
        }

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="string"/> Amazon Web Service access key.
        /// </summary>
        public string AWSAccessKey
        {
            get
            {
                return this.awsAccessKey;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> Amazon Web Service secret key.
        /// </summary>
        public string AWSSecretKey
        {
            get
            {
                return this.awsSecretKey;
            }
        }

        #endregion Public Properties

        #region File And Directory Methods

        /// <summary>
        /// Determines if a files exists on the Amazon S3 server.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <returns>A <see cref="bool"/> indicating if the file exists.</returns>
        public bool FileExists(string bucketName, string s3Key)
        {
            bool exists = true;
            GetObjectMetadataRequest request = new GetObjectMetadataRequest();
            request.BucketName = bucketName;
            request.Key = s3Key;

            using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
            {
                try
                {
                    GetObjectMetadataResponse response = amazonS3Client.GetObjectMetadata(request);
                }
                catch (AmazonS3Exception ex)
                {
                    if (ex.ErrorCode == "NoSuchKey")
                    {
                        exists = false;
                    }
                    else
                    {
                        //Expected ErrorCode was not found, so throw an exception
                        throw new ApplicationException(ex.Message, ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }

            return exists;
        }

        /// <summary>
        /// Gets the list of <see cref="S3Object"/> obejcts from the ListObjectsResponse of all the objects in a bucket that match the specified prefix.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="prefix">The <see cref="string"/> prefix that all object names must have.</param>
        /// <returns>A list of <see cref="S3Object"/> from the Amazon S3 service.</returns>
        public List<S3Object> GetBucketObjectsListXml(string bucketName, string prefix)
        {
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest();
            listObjectsRequest.BucketName = bucketName;
            listObjectsRequest.Prefix = prefix;
            listObjectsRequest.Delimiter = "/";

            ListObjectsResponse listObjectsResponse = new ListObjectsResponse();

            using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
            {
                try
                {
                    listObjectsResponse = amazonS3Client.ListObjects(listObjectsRequest);
                }
                catch (AmazonS3Exception ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }

            return listObjectsResponse.S3Objects;
        }

        /// <summary>
        /// Get a file from a bucket on Amazon S3
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="sourceS3Key">The <see cref="string"/> S3 key for the source file.</param>
        /// <returns>A <see cref="MemoryStream"/> of the file from Amazon S3.</returns>
        public MemoryStream GetFile(string bucketName, string sourceS3Key)
        {
            // Get the file.
            GetObjectRequest getObjectRequest = new GetObjectRequest();
            getObjectRequest.BucketName = bucketName;
            getObjectRequest.Key = sourceS3Key;
            GetObjectResponse getObjectResponse = new GetObjectResponse();
            using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
            {
                try
                {
                    getObjectResponse = amazonS3Client.GetObject(getObjectRequest);
                    // getObjectResponse.ResponseStream is not seekable and
                    // getObjectResponse.ResponseStream.Length will throw an exception
                    Stream responseStream = getObjectResponse.ResponseStream;
                    MemoryStream memoryStream = new MemoryStream();// Memory stream is seekable

                    // We'll read the response stream 1,024 bytes at a time.
                    byte[] responseBuffer = new byte[1024];

                    try
                    {
                        int bytesRead = responseStream.Read(responseBuffer, 0, responseBuffer.Length);
                        while (bytesRead > 0)
                        {
                            // read the file from S3 into the memory
                            memoryStream.Write(responseBuffer, 0, bytesRead);
                            bytesRead = responseStream.Read(responseBuffer, 0, responseBuffer.Length);
                        }
                    }
                    finally
                    {
                        responseStream.Close();
                    }

                    // Reset the position of the stream to the beginning so it's ready to be read from.
                    memoryStream.Position = 0;
                    return memoryStream;
                }
                catch (AmazonS3Exception ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Copies a file to a new location within a bucket on the Amazon S3 server.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="sourceS3Key">The <see cref="string"/> S3 key for the source file.</param>
        /// <param name="destinationS3Key">The <see cref="string"/> S3 key for the destination file.</param>
        public void CopyFile(string bucketName, string sourceS3Key, string destinationS3Key)
        {
            if (!String.IsNullOrEmpty(sourceS3Key) && sourceS3Key.StartsWith("/"))
            {
                // The S3 key can't start with "/" so trim it off
                sourceS3Key = sourceS3Key.Substring(1);
            }

            if (!String.IsNullOrEmpty(sourceS3Key) && destinationS3Key.StartsWith("/"))
            {
                // The S3 key can't start with "/" so trim it off
                destinationS3Key = destinationS3Key.Substring(1);
            }

            if (String.IsNullOrEmpty(sourceS3Key))
            {
                throw new ApplicationException("The sourceS3Key cannot be empty or null.");
            }

            if (String.IsNullOrEmpty(destinationS3Key))
            {
                throw new ApplicationException("The destinationS3Key cannot be empty or null.");
            }

            if (sourceS3Key.ToLower() != destinationS3Key.ToLower())
            {
                // Get the ACL of the file to be renamed.
                GetACLRequest getAclRequest = new GetACLRequest();
                getAclRequest.BucketName = bucketName;
                getAclRequest.Key = sourceS3Key;
                GetACLResponse getAclResponse = new GetACLResponse();
                using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
                {
                    try
                    {
                        getAclResponse = amazonS3Client.GetACL(getAclRequest);
                    }
                    catch (AmazonS3Exception ex)
                    {
                        throw new ApplicationException(ex.Message, ex);
                    }
                }

                CopyObjectRequest copyRequest = new CopyObjectRequest();
                copyRequest.SourceBucket = bucketName;
                copyRequest.SourceKey = sourceS3Key;
                copyRequest.DestinationBucket = bucketName;
                copyRequest.DestinationKey = destinationS3Key;

                CopyObjectResponse copyResponse = new CopyObjectResponse();

                using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
                {
                    try
                    {
                        copyResponse = amazonS3Client.CopyObject(copyRequest);
                    }
                    catch (AmazonS3Exception ex)
                    {
                        throw new ApplicationException(ex.Message, ex);
                    }
                }

                // So set the ACL on the remaned file.
                PutACLRequest putAclRequest = new PutACLRequest();
                putAclRequest.BucketName = bucketName;
                putAclRequest.Key = destinationS3Key;
                putAclRequest.AccessControlList = getAclResponse.AccessControlList;
                PutACLResponse putAclResponse = new PutACLResponse();
                using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
                {
                    try
                    {
                        putAclResponse = amazonS3Client.PutACL(putAclRequest);
                    }
                    catch (AmazonS3Exception ex)
                    {
                        throw new ApplicationException(ex.Message, ex);
                    }
                }
            }
        }

        /// <summary>
        /// Renames a file on the Amazon S3 server.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="sourceS3Key">The <see cref="string"/> S3 key for the source file.</param>
        /// <param name="destinationS3Key">The <see cref="string"/> S3 key for the destination file.</param>
        public void RenameFile(string bucketName, string sourceS3Key, string destinationS3Key)
        {
            this.CopyFile(bucketName, sourceS3Key, destinationS3Key);
            this.DeleteFile(bucketName, sourceS3Key);
        }

        /// <summary>
        /// Deletes a file from Amazon S3 if it exists.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        public void DeleteFile(string bucketName, string s3Key)
        {
            if (!String.IsNullOrEmpty(s3Key) && s3Key.StartsWith("/"))
            {
                // The S3 key can't start with "/" so trim it off
                s3Key = s3Key.Substring(1);
            }

            if (!String.IsNullOrEmpty(s3Key))
            {
                if (this.FileExists(bucketName, s3Key))
                {
                    DeleteObjectRequest deleteRequest = new DeleteObjectRequest();
                    deleteRequest.BucketName = bucketName;
                    deleteRequest.Key = s3Key;
                    DeleteObjectResponse deleteResponse = new DeleteObjectResponse();

                    using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
                    {
                        try
                        {
                            deleteResponse = amazonS3Client.DeleteObject(deleteRequest);
                        }
                        catch (AmazonS3Exception ex)
                        {
                            throw new ApplicationException(ex.Message, ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a directory on Amazon S3.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> canned access client list to be applied to the new directory.</param>
        public void CreateDirectory(string bucketName, string s3Key, AmazonS3CannedACL amazonS3CannedACL)
        {
            if (s3Key.StartsWith("/"))
            {
                // The S3 key can't start with "/" so trim it off
                s3Key = s3Key.Substring(1);
            }

            // If an existing file exits then rename it.
            if (this.FileExists(bucketName, s3Key))
            {
                throw new ApplicationException("A file already exists for the desired key (" + s3Key + ") and deleting the existing file was not allowed.");
            }

            // Upload the video to S3
            PutObjectRequest putRequest = new PutObjectRequest();
            putRequest.BucketName = bucketName;
            putRequest.Key = s3Key;
            putRequest.CannedACL = this.GetS3CannedACL(amazonS3CannedACL);

            PutObjectResponse putResponse = new PutObjectResponse();

            using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
            {
                try
                {
                    putResponse = amazonS3Client.PutObject(putRequest);
                }
                catch (AmazonS3Exception ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Uploads a file to Amazon S3 with a default timeout of 2 minutes.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="file">The <see cref="byte"/> array of the file to be uploaded.</param>
        /// <param name="contentType">The <see cref="string"/> content type of the file.</param>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> canned access client list to be applied to the uploaded file.</param>
        /// <param name="deleteExistingFile">A <see cref="bool"/>indicating if the existing file should be deleted if it exists.</param>
        public void UploadFile(string bucketName, string s3Key, byte[] file, string contentType, AmazonS3CannedACL amazonS3CannedACL, bool deleteExistingFile)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(file);
            this.UploadFile(bucketName, s3Key, memoryStream, contentType, amazonS3CannedACL, deleteExistingFile, 120000);
        }

        /// <summary>
        /// Uploads a file to Amazon S3 with a default timeout of 2 minutes.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="filePath">The <see cref="string"/> file path of the file to be uploaded.</param>
        /// <param name="contentType">The <see cref="string"/> content type of the file.</param>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> canned access client list to be applied to the uploaded file.</param>
        /// <param name="deleteExistingFile">A <see cref="bool"/>indicating if the existing file should be deleted if it exists.</param>
        public void UploadFile(string bucketName, string s3Key, string filePath, string contentType, AmazonS3CannedACL amazonS3CannedACL, bool deleteExistingFile)
        {
            this.UploadFile(bucketName, s3Key, filePath, contentType, amazonS3CannedACL, deleteExistingFile, 120000);
        }

        /// <summary>
        /// Uploads a file to Amazon S3.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="filePath">The <see cref="string"/> file path of the file to be uploaded.</param>
        /// <param name="contentType">The <see cref="string"/> content type of the file.</param>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> canned access client list to be applied to the uploaded file.</param>
        /// <param name="deleteExistingFile">A <see cref="bool"/>indicating if the existing file should be deleted if it exists.</param>
        /// <param name="timeoutInMilliseconds">The <see cref="int"/> number of milliseconds to wait before aborting the upload operation.</param>
        public void UploadFile(string bucketName, string s3Key, string filePath, string contentType, AmazonS3CannedACL amazonS3CannedACL, bool deleteExistingFile, int timeoutInMilliseconds)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            byte[] file = new byte[fileStream.Length];
            fileStream.Read(file, 0, (int)fileStream.Length);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(file);
            this.UploadFile(bucketName, s3Key, memoryStream, contentType, amazonS3CannedACL, deleteExistingFile, timeoutInMilliseconds);
        }

        /// <summary>
        /// Uploads a file to Amazon S3.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="memoryStream">The <see cref="System.IO.MemoryStream"/> of the file to be uploaded.</param>
        /// <param name="contentType">The <see cref="string"/> content type of the file.</param>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> canned access client list to be applied to the uploaded file.</param>
        /// <param name="deleteExistingFile">A <see cref="bool"/>indicating if the existing file should be deleted if it exists.</param>
        public void UploadFile(string bucketName, string s3Key, System.IO.MemoryStream memoryStream, string contentType, AmazonS3CannedACL amazonS3CannedACL, bool deleteExistingFile)
        {
            this.UploadFile(bucketName, s3Key, memoryStream, contentType, amazonS3CannedACL, deleteExistingFile, 120000);
        }

        /// <summary>
        /// Uploads a file to Amazon S3.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="memoryStream">The <see cref="System.IO.MemoryStream"/> of the file to be uploaded.</param>
        /// <param name="contentType">The <see cref="string"/> content type of the file.</param>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> canned access client list to be applied to the uploaded file.</param>
        /// <param name="deleteExistingFile">A <see cref="bool"/>indicating if the existing file should be deleted if it exists.</param>
        /// <param name="timeoutInMilliseconds">The <see cref="int"/> number of milliseconds to wait before aborting the upload operation.</param>
        public void UploadFile(string bucketName, string s3Key, System.IO.MemoryStream memoryStream, string contentType, AmazonS3CannedACL amazonS3CannedACL, bool deleteExistingFile, int timeoutInMilliseconds)
        {
            if (s3Key.StartsWith("/"))
            {
                // The S3 key can't start with "/" so trim it off
                s3Key = s3Key.Substring(1);
            }

            string originalFileTempS3Key = "";

            // If an existing file exits then rename it.
            if (this.FileExists(bucketName, s3Key))
            {
                if (deleteExistingFile)
                {
                    originalFileTempS3Key = this.GetNextAvailableVersionName(bucketName, s3Key);
                    this.RenameFile(bucketName, s3Key, originalFileTempS3Key);
                }
                else
                {
                    throw new ApplicationException("A file already exists for the desired key (" + s3Key + ") and deleting the existing file was not allowed.");
                }
            }

            // Upload the video to S3
            PutObjectRequest putRequest = new PutObjectRequest();
            putRequest.Timeout = new TimeSpan(0, 0, 0, 0, timeoutInMilliseconds);
            putRequest.BucketName = bucketName;
            putRequest.Key = s3Key;
            if (!string.IsNullOrEmpty(contentType))
            {
                putRequest.ContentType = contentType;
            }
            putRequest.InputStream = memoryStream;
            putRequest.CannedACL = this.GetS3CannedACL(amazonS3CannedACL);

            PutObjectResponse putResponse = new PutObjectResponse();

            using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
            {
                try
                {
                    putResponse = amazonS3Client.PutObject(putRequest);

                    // Delete the temp file if we renamed the existing file.
                    if (!String.IsNullOrEmpty(originalFileTempS3Key))
                    {
                        this.DeleteFile(bucketName, originalFileTempS3Key);
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    // If we encountered an error then restore the original file if there was one.
                    if (!String.IsNullOrEmpty(originalFileTempS3Key))
                    {
                        this.DeleteFile(bucketName, s3Key);
                        this.RenameFile(bucketName, originalFileTempS3Key, s3Key);
                    }

                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> S3 key of the next available versioned file name.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <returns>A <see cref="string"/> S3 key of the next available versioned file name.</returns>
        private string GetNextAvailableVersionName(string bucketName, string s3Key)
        {
            string nextVersionName = "";
            string filePathWithoutExt = s3Key.Substring(0, s3Key.LastIndexOf("."));
            string extension = s3Key.Substring(s3Key.LastIndexOf("."));

            for (int i = 1; i < 100; i++)
            {
                nextVersionName = filePathWithoutExt + "_" + i.ToString() + extension;

                if (!this.FileExists(bucketName, nextVersionName))
                {
                    break;
                }
            }

            return nextVersionName;
        }

        #endregion File And Directory Methods

        #region Security

        #region Signed URLs

        /// <summary>
        /// Gets a temporary URL to a S3 object that expires at a specific date/time.
        /// </summary>
        /// <param name="bucketName">The <see cref="string"/> bucket name.</param>
        /// <param name="s3Key">The <see cref="string"/> S3 key for the file.</param>
        /// <param name="expirationDateTime">The <see cref="DateTime"/> when the URL should expire.</param>
        /// <param name="isHttps">A <see cref="bool"/> that indicates if the URL should use the HTTPS protocol.</param>
        /// <returns>A <see cref="string"/> temporary URL to the specified S3 object.</returns>
        public string GetTempUrl(string bucketName, string s3Key, DateTime expirationDateTime, bool isHttps)
        {
            string tempUrl = "";

            GetPreSignedUrlRequest getPreSignedUrlRequest = new GetPreSignedUrlRequest();
            getPreSignedUrlRequest.BucketName = bucketName;
            getPreSignedUrlRequest.Expires = expirationDateTime;
            getPreSignedUrlRequest.Key = s3Key;
            getPreSignedUrlRequest.Protocol = isHttps ? Protocol.HTTPS : Protocol.HTTP;

            using (AmazonS3Client amazonS3Client = new AmazonS3Client(this.awsAccessKey, this.awsSecretKey))
            {
                try
                {
                    tempUrl = amazonS3Client.GetPreSignedURL(getPreSignedUrlRequest);
                }
                catch (AmazonS3Exception ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }

            return tempUrl;
        }

        /// <summary>
        /// Get a signed url from Amazon Web Service Cloudfront
        /// </summary>
        /// <returns></returns>
        public static string GetTempUrlSigned(string urlString, string AWSPrivateKeyPath, string AWSPrivateKey)
        {
            //Urls persist for 5 minutes... testing

            string signedUrl = "", durationUnits = "minutes", durationNumber = "5";
            string privateKeyId = AWSPrivateKey, pathToPrivateKey = AWSPrivateKeyPath;

            signedUrl = CreateCannedPrivateURL(urlString, durationUnits, durationNumber, pathToPrivateKey, privateKeyId);

            return signedUrl;
        }

        /// <summary>
        /// Helper method for signed Urls
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
		public static string ToUrlSafeBase64String(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('=', '_')
                .Replace('/', '~');
        }

        /// <summary>
        /// Creates the signed Url
        /// </summary>
        /// <param name="urlString"></param>
        /// <param name="durationUnits"></param>
        /// <param name="durationNumber"></param>
        /// <param name="pathToPrivateKey"></param>
        /// <param name="privateKeyId"></param>
        /// <returns></returns>
		public static string CreateCannedPrivateURL(string urlString, string durationUnits, string durationNumber, string pathToPrivateKey, string privateKeyId)
        {
            // args[] 0-thisMethod, 1-resourceUrl, 2-seconds-minutes-hours-days
            // to expiration, 3-numberOfPreviousUnits, 4-pathToPolicyStmnt,
            // 5-pathToPrivateKey, 6-PrivateKeyId

            TimeSpan timeSpanInterval = GetDuration(durationUnits, durationNumber);

            // Create the policy statement.
            string strPolicy = CreatePolicyStatement(urlString, DateTime.UtcNow.Add(timeSpanInterval), "0.0.0.0/0");

            if ("Error!" == strPolicy)
            {
                return "Invalid time frame. Start time cannot be greater than end time.";
            }

            // Copy the expiration time defined by policy statement.
            string strExpiration = CopyExpirationTimeFromPolicy(strPolicy);

            // Read the policy into a byte buffer.
            byte[] bufferPolicy = Encoding.ASCII.GetBytes(strPolicy);

            // Initialize the SHA1CryptoServiceProvider object and hash the policy data.
            using (SHA1CryptoServiceProvider cryptoSHA1 = new SHA1CryptoServiceProvider())
            {
                bufferPolicy = cryptoSHA1.ComputeHash(bufferPolicy);

                // Initialize the RSACryptoServiceProvider object.
                RSACryptoServiceProvider providerRSA = new RSACryptoServiceProvider();
                XmlDocument xmlPrivateKey = new XmlDocument();

                // Load PrivateKey.xml, which you created by converting your
                // .pem file to the XML format that the .NET framework uses.
                // Several tools are available. We used
                // .NET 2.0 OpenSSL Public and Private Key Parser,
                // http://www.jensign.com/opensslkey/opensslkey.cs.
                xmlPrivateKey.Load(pathToPrivateKey);

                // Format the RSACryptoServiceProvider providerRSA and
                // create the signature.
                providerRSA.FromXmlString(xmlPrivateKey.InnerXml);
                RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(providerRSA);
                rsaFormatter.SetHashAlgorithm("SHA1");
                byte[] signedPolicyHash = rsaFormatter.CreateSignature(bufferPolicy);

                // Convert the signed policy to URL-safe base64 encoding and
                // replace unsafe characters + = / with the safe characters - _ ~
                string strSignedPolicy = ToUrlSafeBase64String(signedPolicyHash);

                // Concatenate the URL, the timestamp, the signature,
                // and the key pair ID to form the signed URL.
                return urlString + "?Expires=" + strExpiration + "&Signature=" + strSignedPolicy + "&Key-Pair-Id=" + privateKeyId;
            }
        }

        /// <summary>
        /// Creates a policy statement for use in the signed Urls
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <param name="endTime"></param>
        /// <param name="ipAddress"></param>
        /// <returns>The Canned Policy Statement for the requested URL</returns>
		public static string CreatePolicyStatement(string resourceUrl, DateTime endTime, string ipAddress)
        {
            //Canned Policy Statement Template
            string CannedPolicyStatementTemplate = "{\"Statement\":[{\"Resource\":\"RESOURCE_FILL\",\"Condition\":{\"DateLessThan\":{\"AWS:EpochTime\":EXPIRES_FILL}}}]}";

            // Create the policy statement.
            //TimeSpan endTimeSpanFromNow = (endTime - DateTime.Now);
            //TimeSpan intervalEnd = (DateTime.UtcNow.Add(endTimeSpanFromNow)) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            //int endTimestamp = (int)intervalEnd.TotalSeconds;  // END_TIME

            int endTimeUNIX = (int)ConvertToTimestamp(endTime);

            // Replace variables in the policy statement.
            CannedPolicyStatementTemplate = CannedPolicyStatementTemplate.Replace("RESOURCE_FILL", resourceUrl);
            CannedPolicyStatementTemplate = CannedPolicyStatementTemplate.Replace("EXPIRES_FILL", endTimeUNIX.ToString());

            return Regex.Replace(CannedPolicyStatementTemplate, @"\s+", ""); //no whitespace allowed in policy statement
        }

        /// <summary>
        /// Helper method for signed Urls
        /// </summary>
        /// <param name="units"></param>
        /// <param name="numUnits"></param>
        /// <returns></returns>
		public static TimeSpan GetDuration(string units, string numUnits)
        {
            TimeSpan timeSpanInterval = new TimeSpan();
            switch (units)
            {
                case "seconds":
                    timeSpanInterval = new TimeSpan(0, 0, 0, int.Parse(numUnits));
                    break;

                case "minutes":
                    timeSpanInterval = new TimeSpan(0, 0, int.Parse(numUnits), 0);
                    break;

                case "hours":
                    timeSpanInterval = new TimeSpan(0, int.Parse(numUnits), 0, 0);
                    break;

                case "days":
                    timeSpanInterval = new TimeSpan(int.Parse(numUnits), 0, 0, 0);
                    break;

                default:
                    Console.WriteLine("Invalid time units; use seconds, minutes, hours, or days");
                    break;
            }

            return timeSpanInterval;
        }

        private static TimeSpan GetDurationByUnits(string durationUnits,
           string startIntervalFromNow)
        {
            switch (durationUnits)
            {
                case "seconds":
                    return new TimeSpan(0, 0, int.Parse(startIntervalFromNow));

                case "minutes":
                    return new TimeSpan(0, int.Parse(startIntervalFromNow), 0);

                case "hours":
                    return new TimeSpan(int.Parse(startIntervalFromNow), 0, 0);

                case "days":
                    return new TimeSpan(int.Parse(startIntervalFromNow), 0, 0, 0);

                default:
                    return new TimeSpan(0, 0, 0, 0);
            }
        }

        public static string CopyExpirationTimeFromPolicy(string policyStatement)
        {
            int startExpiration = policyStatement.IndexOf("EpochTime");
            string strExpirationRough = policyStatement.Substring(startExpiration + "EpochTime".Length);
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            List<char> listDigits = new List<char>(digits);
            StringBuilder buildExpiration = new StringBuilder(20);

            foreach (char c in strExpirationRough)
            {
                if (listDigits.Contains(c))
                {
                    buildExpiration.Append(c);
                }
            }

            return buildExpiration.ToString();
        }

        private static double ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0));

            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

        #endregion Signed URLs

        /// <summary>
        /// Gets a <see cref="S3CannedACL"/> enum value based on the <see cref="AmazonS3CannedACL"/> provided.
        /// </summary>
        /// <param name="amazonS3CannedACL">The <see cref="AmazonS3CannedACL"/> to convert from.</param>
        /// <returns>A <see cref="S3CannedACL"/> enum value based on the <see cref="AmazonS3CannedACL"/> provided.</returns>
        private S3CannedACL GetS3CannedACL(AmazonS3CannedACL amazonS3CannedACL)
        {
            switch (amazonS3CannedACL)
            {
                case AmazonS3CannedACL.AuthenticatedRead:
                    return S3CannedACL.AuthenticatedRead;

                case AmazonS3CannedACL.BucketOwnerFullControl:
                    return S3CannedACL.BucketOwnerFullControl;

                case AmazonS3CannedACL.BucketOwnerRead:
                    return S3CannedACL.BucketOwnerRead;

                case AmazonS3CannedACL.NoACL:
                    return S3CannedACL.NoACL;

                case AmazonS3CannedACL.Private:
                    return S3CannedACL.Private;

                case AmazonS3CannedACL.PublicRead:
                    return S3CannedACL.PublicRead;

                case AmazonS3CannedACL.PublicReadWrite:
                    return S3CannedACL.PublicReadWrite;

                default:
                    return S3CannedACL.NoACL;
            }
        }

        #endregion Security

        #region Error Handling

        private void SetAmazonS3Errors()
        {
            this.amazonS3Errors = new List<AmazonS3Error>();
            this.amazonS3Errors.Add(new AmazonS3Error("AccessDenied", "Access Denied", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("AccountProblem", "There is a problem with your AWS account that prevents the operation from completing successfully. Please use Contact Us.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("AmbiguousGrantByEmailAddress", "The e-mail address you provided is associated with more than one account.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("BadDigest", "The Content-MD5 you specified did not match what we received.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("BucketAlreadyExists", "The requested bucket name is not available. The bucket namespace is shared by all users of the system. Please select a different name and try again.", HttpStatusCode.Conflict, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("BucketAlreadyOwnedByYou", "Your previous request to create the named bucket succeeded and you already own it.", HttpStatusCode.Conflict, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("BucketNotEmpty", "The bucket you tried to delete is not empty.", HttpStatusCode.Conflict, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("CredentialsNotSupported", "This request does not support credentials.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("CrossLocationLoggingProhibited", "Cross location logging not allowed. Buckets in one geographic location cannot log information to a bucket in another location.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("EntityTooSmall", "Your proposed upload is smaller than the minimum allowed object size.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("EntityTooLarge", "Your proposed upload exceeds the maximum allowed object size.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("ExpiredToken", "The provided token has expired.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("IllegalVersioningConfigurationException", "Indicates that the Versioning configuration specified in the request is invalid.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("IncompleteBody", "You did not provide the number of bytes specified by the Content-Length HTTP header", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("IncorrectNumberOfFilesInPostRequest", "POST requires exactly one file upload per request.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InlineDataTooLarge", "Inline data exceeds the maximum allowed size.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InternalError", "We encountered an internal error. Please try again.", HttpStatusCode.InternalServerError, "Server"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidAccessKeyId", "The AWS Access Key Id you provided does not exist in our records.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidAddressingHeader", "You must specify the Anonymous role.", null, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidArgument", "Invalid Argument", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidBucketName", "The specified bucket is not valid.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidBucketState", "The request is not valid with the current state of the bucket.", HttpStatusCode.Conflict, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidDigest", "The Content-MD5 you specified was an invalid.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidLocationConstraint", "The specified location constraint is not valid. For more information about Regions, see How to Select a Region for Your Buckets.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidPart", "One or more of the specified parts could not be found. The part might not have been uploaded, or the specified entity tag might not have matched the part's entity tag.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidPartOrder", "The list of parts was not in ascending order.Parts list must specified in order by part number.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidPayer", "All access to this object has been disabled.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidPolicyDocument", "The content of the form does not meet the conditions specified in the policy document.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidRange", "The requested range cannot be satisfied.", HttpStatusCode.RequestedRangeNotSatisfiable, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidRequest", "SOAP requests must be made over an HTTPS connection.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidSecurity", "The provided security credentials are not valid.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidSOAPRequest", "The SOAP request body is invalid.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidStorageClass", "The storage class you specified is not valid.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidTargetBucketForLogging", "The target bucket for logging does not exist, is not owned by you, or does not have the appropriate grants for the log-delivery group.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidToken", "The provided token is malformed or otherwise invalid.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("InvalidURI", "Couldn't parse the specified URI.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("KeyTooLong", "Your key is too long.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MalformedACLError", "The XML you provided was not well-formed or did not validate against our published schema.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MalformedPOSTRequest", "The body of your POST request is not well-formed multipart/form-data.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MalformedXML", "This happens when the user sends a malformed xml (xml that doesn't conform to the published xsd) for the configuration. The error message is, \"The XML you provided was not well-formed or did not validate against our published schema.\"", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MaxMessageLengthExceeded", "Your request was too big.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MaxPostPreDataLengthExceededError", "Your POST request fields preceding the upload file were too large.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MetadataTooLarge", "Your metadata headers exceed the maximum allowed metadata size.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MethodNotAllowed", "The specified method is not allowed against this resource.", HttpStatusCode.MethodNotAllowed, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MissingAttachment", "A SOAP attachment was expected, but none were found.", null, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MissingContentLength", "You must provide the Content-Length HTTP header.", HttpStatusCode.LengthRequired, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MissingRequestBodyError", "This happens when the user sends an empty xml document as a request. The error message is, \"Request body is empty.\"", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MissingSecurityElement", "The SOAP 1.1 request is missing a security element.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("MissingSecurityHeader", "Your request was missing a required header.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NoLoggingStatusForKey", "There is no such thing as a logging status sub-resource for a key.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NoSuchBucket", "The specified bucket does not exist.", HttpStatusCode.NotFound, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NoSuchKey", "The specified key does not exist.", HttpStatusCode.NotFound, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NoSuchLifecycleConfiguration", "The lifecycle configuration does not exist.", HttpStatusCode.NotFound, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NoSuchUpload", "The specified multipart upload does not exist. The upload ID might be invalid, or the multipart upload might have been aborted or completed.", HttpStatusCode.NotFound, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NoSuchVersion", "Indicates that the version ID specified in the request does not match an existing version.", HttpStatusCode.NotFound, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NotImplemented", "A header you provided implies functionality that is not implemented.", HttpStatusCode.NotImplemented, "Server"));
            this.amazonS3Errors.Add(new AmazonS3Error("NotSignedUp", "Your account is not signed up for the Amazon S3 service. You must sign up before you can use Amazon S3. You can sign up at the following URL: http://aws.amazon.com/s3", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("NotSuchBucketPolicy", "The specified bucket does not have a bucket policy.", HttpStatusCode.NotFound, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("OperationAborted", "A conflicting conditional operation is currently in progress against this resource. Please try again.", HttpStatusCode.Conflict, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("PermanentRedirect", "The bucket you are attempting to access must be addressed using the specified endpoint. Please send all future requests to this endpoint.", HttpStatusCode.MovedPermanently, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("PreconditionFailed", "At least one of the preconditions you specified did not hold.", HttpStatusCode.PreconditionFailed, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("Redirect", "Temporary redirect.", HttpStatusCode.Moved, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("RequestIsNotMultiPartContent", "Bucket POST must be of the enclosure-type multipart/form-data.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("RequestTimeout", "Your socket connection to the server was not read from or written to within the timeout period.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("RequestTimeTooSkewed", "The difference between the request time and the server's time is too large.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("RequestTorrentOfBucketError", "Requesting the torrent file of a bucket is not permitted.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("SignatureDoesNotMatch", "The request signature we calculated does not match the signature you provided. Check your AWS Secret Access Key and signing method. For more information, see REST Authentication and SOAP Authentication for details.", HttpStatusCode.Forbidden, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("ServiceUnavailable", "Please reduce your request rate.", HttpStatusCode.ServiceUnavailable, "Server"));
            this.amazonS3Errors.Add(new AmazonS3Error("SlowDown", "Please reduce your request rate.", HttpStatusCode.ServiceUnavailable, "Server"));
            this.amazonS3Errors.Add(new AmazonS3Error("TemporaryRedirect", "You are being redirected to the bucket while DNS updates.", HttpStatusCode.Moved, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("TokenRefreshRequired", "The provided token must be refreshed.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("TooManyBuckets", "You have attempted to create more buckets than allowed.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("UnexpectedContent", "This request does not support content.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("UnresolvableGrantByEmailAddress", "The e-mail address you provided does not match any account on record.", HttpStatusCode.BadRequest, "Client"));
            this.amazonS3Errors.Add(new AmazonS3Error("UserKeyMustBeSpecified", "The bucket POST must contain the specified field name. If it is specified, please check the order of the fields.", HttpStatusCode.BadRequest, "Client"));
        }

        #endregion Error Handling
    }

    /// <summary>
    /// The possible canned ACLs for the Amazon S3 objects.
    /// </summary>
    public enum AmazonS3CannedACL
    {
        /// <summary>
        /// No Canned ACL is used.
        /// </summary>
        NoACL = 0,

        /// <summary>
        /// Owner gets FULL_CONTROL.  No one else has access rights (default).
        /// </summary>
        Private = 1,

        /// <summary>
        /// Owner gets FULL_CONTROL and the anonymous principal is granted READ access.
        /// If this policy is used on an object, it can be read from a browser with
        /// no authentication.
        /// </summary>
        PublicRead = 2,

        /// <summary>
        /// Owner gets FULL_CONTROL, the anonymous principal is granted READ and WRITE
        /// access.  This can be a useful policy to apply to a bucket, but is generally
        /// not recommended.
        /// </summary>
        PublicReadWrite = 3,

        /// <summary>
        /// Owner gets FULL_CONTROL, and any principal authenticated as a registered
        /// Amazon S3 user is granted READ access.
        /// </summary>
        AuthenticatedRead = 4,

        /// <summary>
        /// Object Owner gets FULL_CONTROL, Bucket Owner gets READ This ACL applies only
        /// to objects and is equivalent to private when used with PUT Bucket. You use
        /// this ACL to let someone other than the bucket owner write content (get full
        /// control) in the bucket but still grant the bucket owner read access to the
        /// objects.
        /// </summary>
        BucketOwnerRead = 5,

        /// <summary>
        /// Object Owner gets FULL_CONTROL, Bucket Owner gets FULL_CONTROL.  This ACL
        /// applies only to objects and is equivalent to private when used with PUT Bucket.
        /// You use this ACL to let someone other than the bucket owner write content
        /// (get full control) in the bucket but still grant the bucket owner full rights
        /// over the objects.
        /// </summary>
        BucketOwnerFullControl = 6
    }

    /// <summary>
    /// A class that contains error information from Amazon S3
    /// </summary>
    public class AmazonS3Error
    {
        private string errorCode;
        private string description;
        private HttpStatusCode? httpStatusCode;
        private string soapFaultCodePrefix;

        /// <summary>
        /// The pulic contructor for the AmazonS3Error class.
        /// </summary>
        /// <param name="errorCode">The <see cref="string"/> error code.</param>
        /// <param name="description">The <see cref="string"/> description.</param>
        /// <param name="httpStatusCode">The <see cref="HttpStatusCode"/>.</param>
        /// <param name="soapFaultCodePrefix">The <see cref="string"/> SOAP fault code prefix.</param>
        public AmazonS3Error(string errorCode, string description, HttpStatusCode? httpStatusCode, string soapFaultCodePrefix)
        {
            this.errorCode = errorCode;
            this.description = description;
            this.httpStatusCode = httpStatusCode;
            this.soapFaultCodePrefix = soapFaultCodePrefix;
        }

        /// <summary>
        /// Gets the <see cref="string"/> error code.
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return this.errorCode;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> error description.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Net.HttpStatusCode"/> HTTP status code.
        /// </summary>
        public HttpStatusCode? HttpStatusCode
        {
            get
            {
                return this.httpStatusCode;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> SOAP fault code prefix.
        /// </summary>
        public string SoapFaultCodePrefix
        {
            get
            {
                return this.soapFaultCodePrefix;
            }
        }
    }
}