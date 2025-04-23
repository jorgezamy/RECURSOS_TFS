using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Renci.SshNet;

namespace recursos.Content
{

    public class uploadFile
    {
        public class sftp
        {
            private static string host = "166.62.114.250";
            private static string username = "e6vj43909125015";
            private static string password = "hr)QsY3.+94-*";
            private static string destinationpath = "../reportes_fortia/";
            private static int port = 22;

            public static void UploadSFTPFile(string sourcefile)
            {
                using (SftpClient client = new SftpClient(host, port, username, password))
                {
                    if (client.IsConnected)
                        client.Disconnect();

                    client.Connect();

                    //client.ChangeDirectory(@"/prueba");
                    using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024;
                        //client.UploadFile(fs,"/prueba/" + Path.GetFileName(sourcefile));
                        //client.UploadFile(fs, "/prueba/aqui.txt");
                        client.UploadFile(fs, destinationpath + Path.GetFileName(sourcefile));
                    }

                    client.Disconnect();

                }           
            }
        }
    }
}