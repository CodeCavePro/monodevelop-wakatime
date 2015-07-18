﻿using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace MonoDevelop.WakaTime
{

    public static class ZipFile
    {
        public static void ExtractToDirectory(string archiveFilenameIn, string outFolder, string password = "") 
        {
            ICSharpCode.SharpZipLib.Zip.ZipFile zf = null;
            try {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ICSharpCode.SharpZipLib.Zip.ZipFile(fs);
                if (!String.IsNullOrEmpty(password)) {
                    zf.Password = password;     // AES encrypted entries are handled automatically
                }
                foreach (ZipEntry zipEntry in zf) {
                    if (!zipEntry.IsFile) {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath)) {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            } finally {
                if (zf != null) {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }

    }
}

