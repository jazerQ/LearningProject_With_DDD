namespace EFcoreLearningProject.LogInfo
{
    public static class StaticResetLogInfo
    {
        private static readonly string path = "C:\\Users\\jazer\\source\\repos\\EFcoreLearningProject\\EFcoreLearningProject\\LogInfo\\logg.txt";

        public static void Reset() 
        {
            if (File.Exists(path)) 
            {
                File.WriteAllText(path, string.Empty);
                return;
            }
            
            throw new Exception("NoFound txt file for Logging");
        }

    }
}
