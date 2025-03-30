namespace APP.Errors
{
    public static class ErrorMessages
    {
        public const string NOT_FOUND_TO_DO_ITEM = "The ToDo item is not found.";
     
        public const string REQUIRED_TO_DO_ITEM_NAME = "The ToDo item name is required.";
        public const string REQUIRED_TO_DO_ITEM_PRIORITY = "The ToDo item priority is required.";

        public const string TOO_LONG_TO_DO_ITEM_NAME = "The ToDo item name is too long.";
        public const string TOO_LONG_TO_DO_ITEM_DESCRIPTION = "The ToDo item description is too long.";

        public const string INVALID_TO_DO_ITEM_PRIORITY = "Invalid ToDo item priority.";

        public const string ALREADY_DONE_TO_DO_ITEM = "The ToDo item is done is already true.";
    }
}
