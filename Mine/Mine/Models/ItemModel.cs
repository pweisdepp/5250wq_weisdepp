namespace Mine.Models
{
    /// <summary>
    /// Item for the Game
    /// </summary>
    public class ItemModel : BaseModel
    {
        public ItemModel()
        {

        }

        public ItemModel(ItemModel data)
        {
            Update(data);
        }
        // Add Unique attributes for Item

        // The value of the item
        public int Value { get; set; } = 0;

        public bool Update(ItemModel data)
        {
            Name = data.Name;
            Description = data.Description;

            Value = data.Value;

            return true;
        }
    }
}