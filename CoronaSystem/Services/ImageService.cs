using CoronaSystem.Models;

namespace CoronaSystem.Services
{
	public static class ImageService
	{
		// images magic bytes
		private static readonly Dictionary<string, List<byte[]>> _fileSignature =
			new Dictionary<string, List<byte[]>>
			{
				{ ".jpeg", new List<byte[]>
				{
					new byte[] { 0xFF, 0xD8, 0xFF},
				}
				},
				{ ".jpg", new List<byte[]>
				{
					new byte[] { 0xFF, 0xD8, 0xFF},
				}
				},
				{ ".bmp", new List<byte[]>
				{
					new byte[] { 0x42, 0x4d },
				}
				},
				{ ".gif", new List<byte[]>
				{
					new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 },
					new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 },
				}
				},
				{ ".png", new List<byte[]>
				{
					new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
				}
				},
				{ ".tif", new List<byte[]>
				{
					new byte[] { 0x49, 0x20, 0x49 },
					new byte[] { 0x49, 0x49, 0x2A, 0x00 },
				}
				},
				{ ".tiff", new List<byte[]>
				{
					new byte[] { 0x49, 0x20, 0x49 },
					new byte[] { 0x49, 0x49, 0x2A, 0x00 },
				}
				},
			};

		public static bool IsLegalImage (FileStream file, String ext)
		{
			using (var reader = new BinaryReader (file))
			{
				var signatures = _fileSignature[ext];
				var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

				return signatures.Any (signature =>
					headerBytes.Take (signature.Length).SequenceEqual (signature));
			}

		}

		public static async Task<UserImage> CreateUserImage (IFormFile image, User user)
		{
			UserImage toReturn = new();
			var ext = Path.GetExtension (image.FileName).ToLowerInvariant ();
			string path = @"./StoreImages";
			if (!Directory.Exists (path))
			{
				DirectoryInfo di = Directory.CreateDirectory(path);
				di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
			}
			toReturn.Guid = Guid.NewGuid ();
			toReturn.Uri = Path.Combine (user.UserId.ToString (), toReturn.Guid.ToString ()) + ext;
			toReturn.Exist = false;
			using (var stream = File.Create (Path.Combine (path, toReturn.Uri)))
			{
				await image.CopyToAsync (stream);
				toReturn.Exist = true;
			}
			if (toReturn.Exist)
				toReturn.LastUse = File.GetLastAccessTime (Path.Combine (path, toReturn.Uri));

			return toReturn;

		}
	}
}
