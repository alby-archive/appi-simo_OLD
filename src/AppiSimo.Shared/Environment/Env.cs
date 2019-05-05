namespace AppiSimo.Shared.Environment
{
    public static class Env
    {
        public static bool IsDebug
        {
            get
            {
                bool isDebug;
                
                #if DEBUG
                    isDebug = true;
                #endif
                
                return isDebug;
            }
        }
    }
}