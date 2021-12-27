using System;
using System.IO;

using Xamarin.Forms;
using System.Threading.Tasks;
using Acr.UserDialogs;

[assembly:Xamarin.Forms.Dependency(typeof(BaseTest.iOS.UploadService))]

namespace BaseTest.iOS
{
	public class UploadService : FuncInterface
	{
		public UploadService ()
		{
		}

		public async Task<string> UploadFile (string FileName)
		{
			var fs = new FileStream (FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			string saveFilename = Guid.NewGuid () + ".jpg";
			try
			{
				var upWS = new sapbi.chi.com.tw.UploadService ();
				upWS.Url = "http://sapbi.chi.com.tw/test/UploadService.asmx";

				int Offset = 0; // starting offset.

				//define the chunk size
				int ChunkSize = 65536; // 64 * 1024 kb

				//define the buffer array according to the chunksize.
				byte[] Buffer = new byte[ChunkSize];

				long FileSize = new FileInfo(FileName).Length; // File size of file being uploaded.
				// reading the file.
				fs.Position = Offset;
				int BytesRead = 0;
				while (Offset != FileSize) // continue uploading the file chunks until offset = file size.
				{
					BytesRead = fs.Read(Buffer, 0, ChunkSize); // read the next chunk 
					// (if it exists) into the buffer. 
					// the while loop will terminate if there is nothing left to read
					// check if this is the last chunk and resize the buffer as needed 
					// to avoid sending a mostly empty buffer 
					// (could be 10Mb of 000000000000s in a large chunk)
					if (BytesRead != Buffer.Length)
					{
						ChunkSize = BytesRead;
						byte[] TrimmedBuffer = new byte[BytesRead];
						Array.Copy(Buffer, TrimmedBuffer, BytesRead);
						Buffer = TrimmedBuffer; // the trimmed buffer should become the new 'buffer'
					}
					// send this chunk to the server. it is sent as a byte[] parameter, 
					// but the client and server have been configured to encode byte[] using MTOM. 
					bool ChunkAppened = upWS.UploadFile(saveFilename, Buffer, Offset);
					if (!ChunkAppened)
					{
						break;
					}
					// Offset is only updated AFTER a successful send of the bytes. 
					Offset += BytesRead; // save the offset position for resume
				}
			}
			catch(Exception ex){
				saveFilename = "";
			}
			finally
			{
				fs.Close();
			}
			return saveFilename;
		}
	}
}

