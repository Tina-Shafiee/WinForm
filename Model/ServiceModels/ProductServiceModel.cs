using Microsoft.Identity.Client;
using Model.DomainModels;


namespace Model.ServiceModels
{
    public class ProductServiceModel
    {

        #region SelectAll
        public List<Product> SelectAll()
        {
            using (var Context = new FinalProject2dbContext())

            {
                try
                {
                    var p = Context.Product.ToList();
                    return p;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                    }
                }
            }
        }
        #endregion

        public void InsertProduct(Product product)
        {
            using (var context = new FinalProject2dbContext())
            {
                try
                {
                    context.Add(product);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }
        }


        public void Remove(Product product)
        {

            using (var context = new FinalProject2dbContext())
            {
                try
                {
                    context.Remove(product);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }
        }

        public void Update(Product product)
        {
            using (var context = new FinalProject2dbContext())
            {
                try
                {
                    var existingProduct = context.Product.Find(product.Id);

                    if (existingProduct != null)
                    { 
                        existingProduct.Title = product.Title;
                        existingProduct.Quantity = product.Quantity;
                        existingProduct.UnitPrice = product.UnitPrice;
                        
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Product not found!");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    context.Dispose();
                }
            }
        }

    }



}


