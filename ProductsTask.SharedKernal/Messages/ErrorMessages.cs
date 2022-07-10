namespace ProductsTask.SharedKernal.Messages
{
    public static class ErrorMessages
    {
        #region Shared Messages
        public static string InternalServerError = nameof(InternalServerError);
        #endregion

        #region Prodcut Messages
        public static string ProductNotFound = nameof(ProductNotFound);
        public static string ProductWasDamaged = nameof(ProductWasDamaged);
        public static string ProductWasSold = nameof(ProductWasSold);
        public static string ChooseProductStatus = nameof(ChooseProductStatus);

        #endregion
    }
}
