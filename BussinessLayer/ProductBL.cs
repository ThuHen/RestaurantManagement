using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
using System.Data.SqlClient;

namespace BussinessLayer
{
    public class ProductBL
    {
        private ProductDL productDL;
        public ProductBL()
        {
            productDL = new ProductDL();
        }
        public Product GetProduct(int id)
        {
            try
            {
                return productDL.GetProduct(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Product> GetProducts()
        {
            try
            {
                return productDL.GetProducts();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Product product)
        {
            try
            {
                return productDL.Add(product);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Del(string id)
        {
            try
            {
                productDL.Del(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Edit(Product product)
        {
            try
            {
                productDL.Edit(product);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
   
}
