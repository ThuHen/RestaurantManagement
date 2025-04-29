using System;

namespace TransferObject
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte[] Image { get; set; }

        public Product(int id, string name, double price, int categoryId, string categoryName , byte[] image)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Image = image;
        }
        public Product(int id, string name, double price, int categoryId, byte[] image)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            Image = image;
        }
        public Product(string name, double price, int categoryId, byte[] image)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;
            Image = image;
        }
    }
}
