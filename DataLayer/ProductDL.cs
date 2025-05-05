using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;


namespace DataLayer
{
    public class ProductDL : DataProvider
    {
        public Product GetProduct(int id)
        {
            string sql = "SELECT * FROM products WHERE ID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@id", id)
    };

            Product product = null;

            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text, parameters); // ✅ truyền parameters
                if (reader.Read())
                {
                    int Id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    double price = Convert.ToDouble(reader["Price"]);
                    int categoryID = Convert.ToInt32(reader["CategoryID"]);
                    byte[] image = reader["Image"] is DBNull ? null : (byte[])reader["Image"];

                    product = new Product(Id, name, price, categoryID, image);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
            return product;
        }


        public List<Product> GetProducts()
        {
            string sql = "SELECT Id, Name, Price, CategoryID, c.catName, p.Image FROM products p INNER JOIN category c ON c.catID = p.CategoryID";
            string id, name, price, categoryId;
            byte[] image = null; // Initialize image variable
            List<Product> products = new List<Product>();

            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    price = reader[2].ToString();
                    categoryId = reader[3].ToString();
                    string catName = reader[4].ToString(); // Read category name from catName column
                    if (!reader.IsDBNull(5)) // Check if the image column is not null
                    {
                        image = (byte[])reader[5];
                    }

                    // Create Product object from the data read
                    Product product = new Product(int.Parse(id), name, double.Parse(price), int.Parse(categoryId), catName, image);
                    products.Add(product);
                }
                reader.Close();
                return products;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public int Add(Product product)
        {
            string sql = "INSERT INTO products (Name, Price, CategoryID, Image) VALUES (@Name, @price, @cat, @img)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@price", product.Price),
                new SqlParameter("@cat", product.CategoryId),
                new SqlParameter("@img", product.Image)
            };
            try
            {
                Connect();
                return MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public void Del(string id)
        {
            string sql = "DELETE FROM products WHERE Id = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };
            try
            {
                Connect();
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public void Edit(Product product)
        {
            string sql = "UPDATE products SET Name = @name, Price = @price, CategoryID = @cat, Image = @img WHERE Id = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", product.Name),
                new SqlParameter("@price", product.Price),
                new SqlParameter("@cat", product.CategoryId),
                new SqlParameter("@img", product.Image),
                new SqlParameter("@id", product.Id)
            };
            try
            {
                Connect();
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }

        }
    }
}
