using System;
using System.Threading.Tasks;

namespace BaseTest
{
	public interface FuncInterface
	{
		Task<string> UploadFile (string FileName);
	}
}

