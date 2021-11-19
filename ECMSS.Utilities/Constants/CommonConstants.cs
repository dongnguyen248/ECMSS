namespace ECMSS.Utilities.Constants
{
    public static class CommonConstants
    {
        public const int MANAGER_ROLE = 1;
        public const int STAFF_ROLE = 2;

        public const int READ_PERMISSION = 1;
        public const int EDIT_PERMISSION = 2;

        public const string FILE_UPLOAD_PATH = Env.IS_DEVELOPMENT ? "C:/Users/Admin/Desktop/FileSS/" : "D:/Web/ECMSS/FileSS/";
    }
}