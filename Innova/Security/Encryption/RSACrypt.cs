using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Innova.Security.Encryption
{
    /// <summary>
    /// Summary description for RSACrypt.
    /// </summary>
    public class RSACrypt
    {
        private string GetTextFileAsString(string fullFilePath)
        {
            using (FileStream fs = File.OpenRead(fullFilePath))
            {
                BinaryReader br = new BinaryReader(fs);
                byte[] FileBytes = br.ReadBytes((int)fs.Length);
                return Encoding.ASCII.GetString(FileBytes);
            }
        }

        #region private data

        private RSACryptoServiceProvider _publicKeyProvider = null;
        private RSACryptoServiceProvider _privateKeyProvider = null;
        private string _publicKeyFullPath = string.Empty;
        private string _privateKeyFullPath = string.Empty;

        #endregion private data

        #region public properties

        /// <summary>
        /// Gets or sets the full path to the public key.
        /// </summary>
        public string PublicKeyFullPath
        {
            get { return _publicKeyFullPath; }
            set { _publicKeyFullPath = value; }
        }

        /// <summary>
        /// Gets or sets the full path to the private key.
        /// </summary>
        public string PrivateKeyFullPath
        {
            get { return _privateKeyFullPath; }
            set { _privateKeyFullPath = value; }
        }

        #endregion public properties

        #region constructors

        /// <summary>
        /// The public constructor for the <see cref="RSACrypt"/> class.
        /// </summary>
        public RSACrypt() : this(string.Empty, string.Empty) { }

        /// <summary>
        /// The public constructor for the <see cref="RSACrypt"/> class.
        /// </summary>
        /// <param name="PublicKeyFullPath">The <see cref="string"/> full path to the public key.</param>
        public RSACrypt(string PublicKeyFullPath) : this(PublicKeyFullPath, string.Empty) { }

        /// <summary>
        /// The public constructor for the <see cref="RSACrypt"/> class.
        /// </summary>
        /// <param name="PublicKeyFullPath">The <see cref="string"/> full path to the public key.</param>
        /// <param name="PrivateKeyFullPath">The <see cref="string"/> full path to the private key.</param>
        public RSACrypt(string PublicKeyFullPath, string PrivateKeyFullPath)
        {
            _publicKeyFullPath = PublicKeyFullPath;
            _privateKeyFullPath = PrivateKeyFullPath;

            if (_publicKeyFullPath != null && _publicKeyFullPath != "")
            {
                LoadPublicKey();
            }

            if (_privateKeyFullPath != null && _privateKeyFullPath != "")
            {
                LoadPrivateKey();
            }
        }

        #endregion constructors

        #region public methods

        #region key-loading

        /// <summary>
        /// Loads the public key.
        /// </summary>
        public void LoadPublicKey()
        {
            if (_publicKeyProvider != null)
            {
                _publicKeyProvider.Clear();
                _publicKeyProvider = null;
            }

            _publicKeyProvider = new RSACryptoServiceProvider();
            _publicKeyProvider.FromXmlString(this.GetTextFileAsString(_publicKeyFullPath));
        }

        /// <summary>
        /// Loads the public key.
        /// </summary>
        /// <param name="FullPathToPublicKeyFile">The <see cref="string"/> full path to the public key.</param>
        public void LoadPublicKey(string FullPathToPublicKeyFile)
        {
            _publicKeyFullPath = FullPathToPublicKeyFile;
            LoadPublicKey();
        }

        /// <summary>
        /// Loads the private key.
        /// </summary>
        public void LoadPrivateKey()
        {
            if (_privateKeyProvider != null)
            {
                _privateKeyProvider.Clear();
                _privateKeyProvider = null;
            }

            _privateKeyProvider = new RSACryptoServiceProvider();
            _privateKeyProvider.FromXmlString(this.GetTextFileAsString(_privateKeyFullPath));
        }

        /// <summary>
        /// Loads the private key.
        /// </summary>
        /// <param name="FullPathToPrivateKeyFile">The <see cref="string"/> full path to the private key.</param>
        public void LoadPrivateKey(string FullPathToPrivateKeyFile)
        {
            _privateKeyFullPath = FullPathToPrivateKeyFile;
            LoadPrivateKey();
        }

        #endregion key-loading

        #region key-generation

        /*
			public static void GenerateAndSaveKeyPair(int RSAKeySize, string FullPathToPublicKeyFile, string FullPathToPrivateKeyFile)
			{
				RSACryptoServiceProvider RsaProvider = new RSACryptoServiceProvider(RSAKeySize);
				string PubKey = RsaProvider.ToXmlString(false);
				string PrivKey = RsaProvider.ToXmlString(true);
				IOHelper.SaveTextFile(PubKey, FullPathToPublicKeyFile);
				IOHelper.SaveTextFile(PrivKey, FullPathToPrivateKeyFile);
			}
			*/

        #endregion key-generation

        #region encryption

        /// <summary>
        /// Performs the encryption
        /// </summary>
        /// <param name="StringToEncrypt">The <see cref="string"/> to encrypt.</param>
        /// <returns>An encrypted <see cref="string"/>.</returns>
        public string Encrypt(string StringToEncrypt)
        {
            if (StringToEncrypt == null)
            {
                return string.Empty;
            }

            if (_publicKeyProvider == null)
            {
                _publicKeyProvider = new RSACryptoServiceProvider();
            }

            _publicKeyProvider.FromXmlString(this.GetTextFileAsString(_publicKeyFullPath));

            int keySizeInBytes = _publicKeyProvider.KeySize / 8;
            int blockSize = keySizeInBytes - 11;
            int iterations = 0;

            if (StringToEncrypt.Length % blockSize != 0)
            {
                iterations = ((int)StringToEncrypt.Length / blockSize) + 1;
            }
            else
            {
                iterations = (int)StringToEncrypt.Length / blockSize;
            }

            byte[] allEncryptedBytes = new byte[iterations * keySizeInBytes];
            char[] dataToEncryptAsChars = StringToEncrypt.ToCharArray();
            int index = 0;
            int counter = 0;
            for (; counter < iterations; index += blockSize, ++counter)
            {
                int doneSoFar = counter * blockSize;
                int endIndex = 0;
                //if 1st iteration and data smaller than block
                if (counter == 0 && dataToEncryptAsChars.Length < blockSize)
                {
                    endIndex = dataToEncryptAsChars.Length;
                }
                else
                {
                    //final block
                    if (counter == iterations - 1)
                    {
                        endIndex = dataToEncryptAsChars.Length % blockSize;
                    }
                    else
                    {
                        endIndex = blockSize;
                    }
                }
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(dataToEncryptAsChars, index, endIndex);
                byte[] encryptedBytes = _publicKeyProvider.Encrypt(buffer, false);
                Array.Copy(encryptedBytes, 0, allEncryptedBytes, counter * keySizeInBytes, keySizeInBytes);
            }

            return Convert.ToBase64String(allEncryptedBytes);
        }

        /// <summary>
        /// Performs the encryption
        /// </summary>
        /// <param name="StringToEncrypt">The <see cref="string"/> to encrypt.</param>
        /// <param name="FullPathToPublicKeyFile">The <see cref="string"/> full path to the public key file.</param>
        /// <returns>An encrypted <see cref="string"/>.</returns>
        public string Encrypt(string StringToEncrypt, string FullPathToPublicKeyFile)
        {
            _publicKeyFullPath = FullPathToPublicKeyFile;
            return Encrypt(StringToEncrypt);
        }

        #endregion encryption

        #region decryption

        /// <summary>
        /// Performs the decryption process.
        /// </summary>
        /// <param name="StringToDecrypt">The encrypted <see cref="string"/> that is to be decrypted.</param>
        /// <returns>A decrypted <see cref="string"/>.</returns>
        public string Decrypt(string StringToDecrypt)
        {
            if (StringToDecrypt == null)
            {
                return string.Empty;
            }

            if (_privateKeyProvider == null)
            {
                _privateKeyProvider = new RSACryptoServiceProvider();
            }

            _privateKeyProvider.FromXmlString(this.GetTextFileAsString(_privateKeyFullPath));

            int keySizeInBytes = _publicKeyProvider.KeySize / 8;

            byte[] dataToDecryptBuffer = Convert.FromBase64String(StringToDecrypt);

            if (dataToDecryptBuffer.Length % keySizeInBytes != 0)
            {
                throw new ApplicationException("Unable to decrypt the data.");
            }

            byte[] allDecryptedBytes = _privateKeyProvider.Decrypt(dataToDecryptBuffer, false);

            return ASCIIEncoding.ASCII.GetString(allDecryptedBytes);
        }

        /// <summary>
        /// Performs the decryption process.
        /// </summary>
        /// <param name="StringToDecrypt">The encrypted <see cref="string"/> that is to be decrypted.</param>
        /// <param name="FullPathToPrivateKey">The <see cref="string"/> full path to the private key.</param>
        /// <returns>A decrypted <see cref="string"/>.</returns>
        public string Decrypt(string StringToDecrypt, string FullPathToPrivateKey)
        {
            _privateKeyFullPath = FullPathToPrivateKey;
            return Decrypt(StringToDecrypt);
        }

        #endregion decryption

        #endregion public methods
    }
}