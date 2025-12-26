namespace RestaurantAPI.Exceptions
{
    public class UnsupportedFileExtensionException : Exception
    {
        public string FileExtension { get; }

        public UnsupportedFileExtensionException(string fileExtension)
            : base($"File externsion '{fileExtension}' is not supported.")

        {
            this.FileExtension = fileExtension;
        }
    }
}
