using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;    // X509Certificate2
using System.IO;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;  //IAsymmetricBlockCipher engine = new RsaEngine();
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.X509; //X509Name
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Utilities.Collections;   //X509V3CertificateGenerator
using Org.BouncyCastle.Asn1.Pkcs;   //PrivateKeyInfo
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Digests;
namespace FrmLib.Encrypt
{
    public enum enumEngineType
    {
        nopad = 0,
        OaepEncoding = 1,
        Pkcs1Encoding = 2,
        ISO9796d1Encoding = 3


    }
    public class RsaBcTools
    {

        private string keyfilename = "rsakey";
        private AsymmetricCipherKeyPair keyPair;
        public int keyLength = 1024;
        IAsymmetricBlockCipher rsaEngine = null;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] getCerFile()
        {

            string password = "V3ry_S3kr37;-)";
            string signatureAlgorithm = "SHA1WithRSA";



            // Generate certificate
            var attributes = new Hashtable();
            attributes[X509Name.E] = "baiyi@company.com";//设置dn信息的邮箱地址
            attributes[X509Name.CN] = "www.baiyi.com";//设置证书的用户，也就是颁发给谁
            attributes[X509Name.O] = "Company baiyi.";//设置证书的办法者
            attributes[X509Name.C] = "Zh";//证书的语言


            //这里是证书颁发者的信息
            var ordering = new ArrayList();
            ordering.Add(X509Name.E);
            ordering.Add(X509Name.CN);
            ordering.Add(X509Name.O);
            ordering.Add(X509Name.C);

            var certificateGenerator = new X509V3CertificateGenerator();

            //设置证书序列化号
            certificateGenerator.SetSerialNumber(BigInteger.ProbablePrime(120, new Random()));

            //设置颁发者dn信息
            certificateGenerator.SetIssuerDN(new X509Name(ordering, attributes));

            //设置证书生效时间
            certificateGenerator.SetNotBefore(DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0)));

            //设置证书失效时间
            certificateGenerator.SetNotAfter(DateTime.Today.AddDays(365));

            //设置接受者dn信息
            certificateGenerator.SetSubjectDN(new X509Name(ordering, attributes));

            //设置证书的公钥
            certificateGenerator.SetPublicKey(keyPair.Public);

            //设置证书的加密算法
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);
            certificateGenerator.AddExtension(X509Extensions.BasicConstraints,
                true,
                new BasicConstraints(false));
            certificateGenerator.AddExtension(X509Extensions.AuthorityKeyIdentifier,
                true,
                new AuthorityKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public)));
            // Key usage: Client authentication
            certificateGenerator.AddExtension(X509Extensions.ExtendedKeyUsage.Id,
                false,
                new ExtendedKeyUsage(new ArrayList() { new DerObjectIdentifier("1.3.6.1.5.5.7.3.2") }));
            var x509Certificate = certificateGenerator.Generate(keyPair.Private);
            byte[] certbyte = DotNetUtilities.ToX509Certificate(x509Certificate).Export(X509ContentType.Cert);
            return certbyte;
            //创建证书，如果需要cer格式的证书，到这里就可以了。如果是pfx格式的就需要加上访问密码
            /*      var x509Certificate = certificateGenerator.Generate(keyPair.Private);
                  byte[] pkcs12Bytes = DotNetUtilities.ToX509Certificate(x509Certificate).Export(X509ContentType.Pkcs12, password);

                  var certificate = new X509Certificate2(pkcs12Bytes, password);

                  // Derive security token and use it
                  var x509Token = new X509SecurityToken(certificate); 
             */

        }
        public string ConvertToPEM(bool isPublicKey)//XML格式密钥转PEM
        {
            /*
            var rsa2 = new RSACryptoServiceProvider();
           
                rsa2.FromXmlString(xmlcontent);
            var p = rsa2.ExportParameters(true);

            var key = new RsaPrivateCrtKeyParameters(
                new BigInteger(1, p.Modulus), new BigInteger(1, p.Exponent), new BigInteger(1, p.D),
                new BigInteger(1, p.P), new BigInteger(1, p.Q), new BigInteger(1, p.DP), new BigInteger(1, p.DQ),
                new BigInteger(1, p.InverseQ));

            using (var sw = new StreamWriter("e:\\PrivateKey.pem"))
            {
                var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(sw);
                pemWriter.WriteObject(key);
               
            }
             */


            using (var sw = new StringWriter())
            {
                var pemWriter = new Org.BouncyCastle.OpenSsl.PemWriter(sw);
                if (isPublicKey)
                    pemWriter.WriteObject(keyPair.Public);
                else
                    pemWriter.WriteObject(keyPair.Private);
                return sw.ToString();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includePrivate">是否包含私钥</param>
        /// <returns></returns>
        public string ConvertToXML(bool includePrivate)
        {
            /*
            AsymmetricCipherKeyPair keyPair;
            using (var sr = new StreamReader("e:\\PrivateKey.pem"))
            {
                var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sr);
                keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
            }
            */
            var key = (RsaPrivateCrtKeyParameters)keyPair.Private;
            var p = new RSAParameters
            {
                Modulus = key.Modulus.ToByteArrayUnsigned(),
                Exponent = key.PublicExponent.ToByteArrayUnsigned(),
                D = key.Exponent.ToByteArrayUnsigned(),
                P = key.P.ToByteArrayUnsigned(),
                Q = key.Q.ToByteArrayUnsigned(),
                DP = key.DP.ToByteArrayUnsigned(),
                DQ = key.DQ.ToByteArrayUnsigned(),
                InverseQ = key.QInv.ToByteArrayUnsigned(),
            };
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(p);
            return rsa.ToXmlString(includePrivate);

        }
        public byte[] getPfxContent()
        {
            char[] passwd = "123456".ToCharArray();   //pfx密码  

            RsaKeyParameters pubKey = (RsaKeyParameters)keyPair.Public; //CA公钥  
            RsaKeyParameters priKey = (RsaKeyParameters)keyPair.Private;    //CA私钥  
            Hashtable attrs = new Hashtable();
            ArrayList order = new ArrayList();
            attrs.Add(X509Name.C, "CN");    //country code  
            attrs.Add(X509Name.O, "South China Normal University"); //organization  
            attrs.Add(X509Name.OU, "South China Normal University");    //organizational unit name              
            attrs.Add(X509Name.CN, "CAcert");   //common name  
            attrs.Add(X509Name.E, "popozhude@qq.com");
            order.Add(X509Name.C);
            order.Add(X509Name.O);
            order.Add(X509Name.OU);
            order.Add(X509Name.CN);
            order.Add(X509Name.E);
            X509Name issuerDN = new X509Name(order, attrs);
            X509Name subjectDN = issuerDN;  //自签证书，两者一样  
            X509V1CertificateGenerator v1certGen = new X509V1CertificateGenerator();
            v1certGen.SetSerialNumber(new BigInteger(128, new Random()));   //128位  
            v1certGen.SetIssuerDN(issuerDN);
            v1certGen.SetNotBefore(DateTime.UtcNow.AddDays(-1));
            v1certGen.SetNotAfter(DateTime.UtcNow.AddDays(365));
            v1certGen.SetSubjectDN(subjectDN);
            v1certGen.SetPublicKey(pubKey); //公钥  
            v1certGen.SetSignatureAlgorithm("SHA1WithRSAEncryption");
            Org.BouncyCastle.X509.X509Certificate CAcert = v1certGen.Generate(priKey);
            CAcert.CheckValidity();
            CAcert.Verify(pubKey);

            //属性包  
            /* 
            Hashtable bagAttr = new Hashtable(); 
            bagAttr.Add(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id, 
                new DerBmpString("CA's Primary Certificate")); 
            bagAttr.Add(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id, 
                new SubjectKeyIdentifierStructure(pubKey)); 
     
            X509CertificateEntry certEntry = new X509CertificateEntry(CAcert,bagAttr); 
            */
            X509CertificateEntry certEntry = new X509CertificateEntry(CAcert);
            Pkcs12Store store = new Pkcs12StoreBuilder().Build();
            store.SetCertificateEntry("CA's Primary Certificate", certEntry);   //设置证书  
            X509CertificateEntry[] chain = new X509CertificateEntry[1];
            chain[0] = certEntry;
            store.SetKeyEntry("CA's Primary Certificate", new AsymmetricKeyEntry(priKey), chain);   //设置私钥  
            MemoryStream stmout = new MemoryStream();
            store.Save(stmout, passwd, new SecureRandom());   //保存  
            return stmout.ToArray();
        }
        public string EnCrypt(string input)
        {
            //一个测试……………………
            //输入，十六进制的字符串，解码为byte[]
            //string input = "4e6f77206973207468652074696d6520666f7220616c6c20676f6f64206d656e";
            //byte[] testData = Org.BouncyCastle.Utilities.Encoders.Hex.Decode(input);           
            // string input = "popozh RSA test";
            byte[] data = Encoding.UTF8.GetBytes(input);
            //非对称加密算法，加解密用
            //公钥加密 
            this.rsaEngine.Init(true, keyPair.Public);
            try
            {
                int blockSize = rsaEngine.GetInputBlockSize();

                List<byte> output = new List<byte>();

                for (int chunkPosition = 0; chunkPosition < data.Length; chunkPosition += blockSize)
                {
                    int chunkSize = Math.Min(blockSize, data.Length - chunkPosition);
                    output.AddRange(rsaEngine.ProcessBlock(data, chunkPosition, chunkSize));
                }
                Console.WriteLine("加密完成！" + Environment.NewLine);


                return Convert.ToBase64String(output.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed - exception " + Environment.NewLine + ex.ToString());
                return null;
            }
        }

        public string deCrypt(string cryText)
        {


            //私钥解密
            //获取加密的私钥，进行解密，获得私钥
            byte[] data = Convert.FromBase64String(cryText);
            //            byte[] anothertestdata = new byte[1024];



            rsaEngine.Init(false, keyPair.Private);
            try
            {
                int blockSize = rsaEngine.GetInputBlockSize();

                List<byte> output = new List<byte>();

                for (int chunkPosition = 0; chunkPosition < data.Length; chunkPosition += blockSize)
                {
                    int chunkSize = Math.Min(blockSize, data.Length - chunkPosition);
                    output.AddRange(rsaEngine.ProcessBlock(data, chunkPosition, chunkSize));
                }
                //               Console.WriteLine("解密后密文为：" + Encoding.UTF8.GetString(output.ToArray()) + Environment.NewLine);
                return Encoding.UTF8.GetString(output.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine("failed - exception " + e.ToString());
                return null;
            }


        }
        public byte[] getPubFileContent()
        {
            //获取公钥和私钥
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;

            if (((RsaKeyParameters)publicKey).Modulus.BitLength < keyLength)
            {
                throw new Exception("failed key generation  length test");
            }
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            Asn1Object aobject = publicKeyInfo.ToAsn1Object();
            byte[] pubInfoByte = aobject.GetEncoded();
            return pubInfoByte;
        }
        public byte[] getPriFileContent()
        {
            //获取公钥和私钥

            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;

            if (((RsaKeyParameters)publicKey).Modulus.BitLength < keyLength)
            {
                throw new Exception("failed key generation () length test");
            }


            string alg = "1.2.840.113549.1.12.1.3"; // 3 key triple DES with SHA-1
            byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int count = 1000;
            char[] password = "123456".ToCharArray();
            EncryptedPrivateKeyInfo enPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(
                alg,
                password,
                salt,
                count,
                privateKey);
            byte[] priInfoByte = enPrivateKeyInfo.ToAsn1Object().GetEncoded();
            return priInfoByte;
        }
        private void createRsaEngine(enumEngineType rsatype)
        {
            switch (rsatype)
            {
                case enumEngineType.nopad:
                    rsaEngine = new RsaEngine();
                    break;
                case enumEngineType.OaepEncoding:
                    rsaEngine = new OaepEncoding(new RsaEngine(), new Sha1Digest());
                    break;
                case enumEngineType.Pkcs1Encoding:
                    rsaEngine = new Pkcs1Encoding(new RsaEngine());
                    break;
                case enumEngineType.ISO9796d1Encoding:
                    rsaEngine = new ISO9796d1Encoding(new RsaEngine());
                    break;

            }
        }
        public RsaBcTools(int length, enumEngineType rsatype)
        {
            //RSA密钥对的构造器
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
            //RSA密钥构造器的参数
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                length,   //密钥长度
                25);
            //用参数初始化密钥构造器
            keyGenerator.Init(param);
            //产生密钥对
            keyPair = keyGenerator.GenerateKeyPair();
            keyLength = length;
            createRsaEngine(rsatype);
        }
        public RsaBcTools(string filename, int length, enumEngineType rsatype, bool savefile = false)
            : this(length, rsatype)
        {
            keyfilename = filename;
            if (savefile)
            {
                FileStream fs;
                fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, keyfilename + ".pub"), FileMode.Create, FileAccess.Write);
                byte[] fcontent = this.getPubFileContent();
                fs.Write(fcontent, 0, fcontent.Length);
                fs.Close();
                fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, keyfilename + ".pri"), FileMode.Create, FileAccess.Write);
                fcontent = this.getPriFileContent();
                fs.Write(fcontent, 0, fcontent.Length);
                fs.Close();

            }
        }
        public RsaBcTools(byte[] pubkey, byte[] prikey, enumEngineType rsatype)
            : this(new MemoryStream(pubkey), new MemoryStream(prikey), rsatype)
        { }
        public RsaBcTools(Stream pubsm, Stream prism, enumEngineType rsatype)
        {
            Asn1Object aobject = Asn1Object.FromStream(pubsm);  //a.puk??
            SubjectPublicKeyInfo pubInfo = SubjectPublicKeyInfo.GetInstance(aobject);
            AsymmetricKeyParameter testpublicKey = (RsaKeyParameters)PublicKeyFactory.CreateKey(pubInfo);

            Asn1Object aobj = Asn1Object.FromStream(prism);   //a.pvk??
            EncryptedPrivateKeyInfo enpri = EncryptedPrivateKeyInfo.GetInstance(aobj);
            char[] password = "123456".ToCharArray();
            PrivateKeyInfo priKey = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, enpri);    //解密
            AsymmetricKeyParameter testpriKey = PrivateKeyFactory.CreateKey(priKey);    //私钥
            keyPair = new AsymmetricCipherKeyPair(testpublicKey, testpriKey);
            keyLength = ((RsaKeyParameters)keyPair.Public).Modulus.BitLength;
            createRsaEngine(rsatype);
        }
        public RsaBcTools(string filename, enumEngineType rsatype)
            : this(new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename + ".pub"), FileMode.Open, FileAccess.Read),
            new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename + ".pri"), FileMode.Open, FileAccess.Read), rsatype)
        {
            keyfilename = filename;

        }

    }

    public class RsaMsTools
    {
        private int _dwKeySize = 1024;
        private string _privateKey;
        private string _publicKey;
        static byte[] String2Bytes(string str)
        {/*
          str = System.Text.RegularExpressions.Regex.Replace(str, pattern, "");
          if (str == string.Empty)
              return null;
          byte[] data = new byte[str.Length / 2];

          for (int i = 0; i < data.Length; ++i)
              data[i] = byte.Parse(str.Substring(2 * i, 2), System.Globalization.NumberStyles.HexNumber);
          return data;
          */
            return Encoding.UTF8.GetBytes(str);
        }
        static string Bytes2String(byte[] data)
        {/*
          System.Text.StringBuilder builder = new System.Text.StringBuilder();
          foreach (var element in data)
          {
              builder.AppendFormat("{0:X2}", element);
          }
          return builder.ToString();
          */
            return Encoding.UTF8.GetString(data);
        }
        public RsaMsTools(int dwkeysize)
        {
            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider(dwkeysize);
                _dwKeySize = dwkeysize;
                _privateKey = provider.ToXmlString(true);
                _publicKey = provider.ToXmlString(false); ;


            }

            catch (Exception exception)
            {

                throw exception;

            }
        }
        public RsaMsTools(int dwkeysize, string prikey, string pubkey)
        {
            _dwKeySize = dwkeysize;

            _privateKey = prikey;
            _publicKey = pubkey;

        }




        /// <summary>

        /// 对原始数据进行MD5加密

        /// </summary>

        /// <param name="m_strSource">待加密数据</param>

        /// <returns>返回机密后的数据</returns>

        public string GetHash(string m_strSource)
        {

            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");

            byte[] bytes = Encoding.UTF8.GetBytes(m_strSource);

            byte[] inArray = algorithm.ComputeHash(bytes);

            return Convert.ToBase64String(inArray);

        }

        /// <summary>

        /// RSA加密

        /// </summary>

        /// <param name="xmlPublicKey">公钥</param>

        /// <param name="m_strEncryptString">MD5加密后的数据</param>

        /// <returns>RSA公钥加密后的数据</returns>

        private string RSAEncrypt(string inputString)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider(this._dwKeySize);
                provider.FromXmlString(_publicKey);
                int keySize = _dwKeySize / 8;
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                int maxLength = keySize - 42;
                int dataLength = bytes.Length;
                int iterations = dataLength / maxLength;
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i <= iterations; i++)
                {
                    byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                    Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                    byte[] encryptedBytes = provider.Encrypt(tempBytes, false); //true:OAEP;false;PCSK1-v1_5
                    // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the DecryptString function.
                    //       Array.Reverse(encryptedBytes);
                    // Why convert to base 64?
                    // Because it is the largest power-of-two base printable using only ASCII characters
                    stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
                }
                return stringBuilder.ToString();


            }

            catch (Exception exception)
            {

                throw exception;

            }



        }

        /// <summary>

        /// RSA解密

        /// </summary>

        /// <param name="xmlPrivateKey">私钥</param>

        /// <param name="m_strDecryptString">待解密的数据</param>

        /// <returns>解密后的结果</returns>

        private string RSADecrypt(string inputString)
        {

            string str2;

            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider(_dwKeySize);
                provider.FromXmlString(_privateKey);
                /*
                byte[] content = Convert.FromBase64String(inputString);
                byte[] cipherbytes;
                cipherbytes = provider.Decrypt(content, false);
                return Encoding.UTF8.GetString(cipherbytes);   
                */
                int base64BlockSize = ((_dwKeySize / 8) % 3 != 0) ? (((_dwKeySize / 8) / 3) * 4) + 4 : ((_dwKeySize / 8) / 3) * 4;
                int iterations = inputString.Length / base64BlockSize;
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i <= iterations; i++)
                {
                    byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
                    // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                    // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                    // Comment out the next line and the corresponding one in the EncryptString function.
                    //   Array.Reverse(encryptedBytes);
                    arrayList.AddRange(provider.Decrypt(encryptedBytes, false));//true:OAEP;false;PCSK1-v1_5
                }
                return Encoding.UTF8.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);

            }

            catch (Exception exception)
            {

                throw exception;

            }

            return str2;

        }

        /// <summary>

        /// 对MD5加密后的密文进行签名

        /// </summary>

        /// <param name="p_strKeyPrivate">私钥</param>

        /// <param name="m_strHashbyteSignature">MD5加密后的密文</param>

        /// <returns></returns>

        public string SignatureFormatter(string m_strHashbyteSignature)
        {

            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);

            RSACryptoServiceProvider key = new RSACryptoServiceProvider(this._dwKeySize);
            key.FromXmlString(_privateKey);


            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);

            formatter.SetHashAlgorithm("MD5");

            byte[] inArray = formatter.CreateSignature(rgbHash);

            return Convert.ToBase64String(inArray);

        }

        /// <summary>

        /// 签名验证

        /// </summary>


        /// <param name="p_strHashbyteDeformatter">待验证的用户名</param>

        /// <param name="p_strDeformatterData">注册码</param>

        /// <returns></returns>

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            try
            {

                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);

                RSACryptoServiceProvider key = new RSACryptoServiceProvider(_dwKeySize);
                key.FromXmlString(_publicKey);


                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);

                deformatter.SetHashAlgorithm("MD5");

                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);

                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {

                    return true;

                }

                return false;

            }

            catch
            {

                return false;

            }

        }



        /// <summary>

        /// 读取公钥

        /// </summary>

        /// <param name="path"></param>

        /// <returns></returns>

        public string ReadPublicKey()
        {



            return _publicKey;

        }

        /// <summary>

        /// 读取私钥

        /// </summary>

        /// <param name="path"></param>

        /// <returns></returns>

        public string ReadPrivateKey()
        {


            return _privateKey;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public string decryptor(string inputString)
        {

            byte[] EncryptDada = Convert.FromBase64String(inputString);
            if (EncryptDada == null || EncryptDada.Length <= 0)
            {
                throw new NotSupportedException();
            }
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(_dwKeySize);
            provider.FromXmlString(this._privateKey);
            int keySize = provider.KeySize / 8;
            byte[] buffer = new byte[keySize];

            using (MemoryStream input = new MemoryStream(EncryptDada))
            using (MemoryStream output = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, keySize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] decrypt = provider.Decrypt(temp, false);
                    output.Write(decrypt, 0, decrypt.Length);
                }
                //  return output.ToArray();
                return Encoding.UTF8.GetString(output.ToArray());
            }
        }


        public string encryptor(string inputString)
        {

            byte[] OriginalData = Encoding.UTF8.GetBytes(inputString);
            if (OriginalData == null || OriginalData.Length <= 0)
            {
                throw new NotSupportedException();
            }

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(this._dwKeySize);
            if (provider == null)
            {
                throw new ArgumentNullException();
            }
            provider.FromXmlString(this._publicKey);

            int bufferSize = (provider.KeySize / 8) - 11;
            byte[] buffer = new byte[bufferSize];
            //分段加密
            using (MemoryStream input = new MemoryStream(OriginalData))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = provider.Encrypt(temp, false);
                   // stringBuilder.Append(Convert.ToBase64String(encrypt));
                     ouput.Write(encrypt, 0, encrypt.Length);
                }
                //return ouput.ToArray();
                return Convert.ToBase64String(ouput.ToArray());
            }
        }
    }
}
