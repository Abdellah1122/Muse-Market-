using System.Drawing;

namespace FananeenAPI.Services
{
    public class ImageComparer
    {
        public static string FindClosestImage(string inputImagePath, string folderPath)
        {
            string inputHash = GetImageHash(inputImagePath);

            int minDistance = int.MaxValue;
            string closestImage = null;

            foreach (var file in Directory.GetFiles(folderPath))
            {
                if (!file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)) continue;

                string currentHash = GetImageHash(file);
                int distance = HammingDistance(inputHash, currentHash);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestImage = Path.GetFileName(file);
                }
            }

            return closestImage;
        }

        private static string GetImageHash(string imagePath)
        {
            using var image = new Bitmap(imagePath);
            using var resized = new Bitmap(image, new Size(8, 8));

            string hash = "";
            double total = 0;
            double[] pixels = new double[64];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color pixel = resized.GetPixel(j, i);
                    double gray = (pixel.R + pixel.G + pixel.B) / 3.0;
                    pixels[i * 8 + j] = gray;
                    total += gray;
                }
            }

            double avg = total / 64;
            foreach (var val in pixels)
            {
                hash += val >= avg ? "1" : "0";
            }

            return hash;
        }

        private static int HammingDistance(string hash1, string hash2)
        {
            return hash1.Zip(hash2, (c1, c2) => c1 == c2 ? 0 : 1).Sum();
        }
    }
}
