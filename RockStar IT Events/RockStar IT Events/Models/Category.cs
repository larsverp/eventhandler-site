namespace RockStar_IT_Events.Models
{
    public class Category
    {
        private string categoryName;

        private string categoryDescription;

        private string categoryThumbnail;

        public Category(string categoryname, string categorydescription, string categorythumbnail)
        {
            categoryName = categoryname;
            categoryDescription = categorydescription;
            categoryThumbnail = categorythumbnail;
        }

        public void SetName(string setCategoryname)
        {
            categoryName = setCategoryname;
        }

        public void SetDescription(string setCategorydescription)
        {
            categoryName = setCategorydescription;
        }

        public void SetThumbnail(string setCategorythumbnail)
        {
            categoryThumbnail = setCategorythumbnail;
        }

    }
}
