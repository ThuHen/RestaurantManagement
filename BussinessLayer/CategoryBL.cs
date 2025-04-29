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
    public class CategoryBL
    {
        private CategoryDL categoryDL;
        public CategoryBL()
        {
            categoryDL = new CategoryDL();
        }
        public List<Category> GetCategories()
        {
            try
            {
                return categoryDL.GetCategories();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public int Add(Category category)
        {
            try
            {
                return categoryDL.Add(category);
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
                categoryDL.Del(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Edit(string id, string name)
        {
            try
            {
                categoryDL.Edit(id, name);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
