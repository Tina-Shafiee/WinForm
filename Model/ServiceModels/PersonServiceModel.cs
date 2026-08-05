using Model.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ServiceModels
{
    public class PersonServiceModel
    {

        #region SelectAll
        public List<Person> SelectAll()
        {
            using (var Context = new FinalProject2dbContext())

            {
                try
                {
                    var p = Context.Person.ToList();
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



        #region Insert
        public void Insert(Person person)
        {
            using (var Context = new FinalProject2dbContext())

            {
                try
                {
                   Context.Add(person);
                   Context.SaveChanges();
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


        public void Remove(Person person)//remove(person )
        {
            using (var Context = new FinalProject2dbContext())
            {
                try
                {
                    Context.Remove(person);
                    Context.SaveChanges();
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

        public void Update(Person person)
        {
            using (var context = new FinalProject2dbContext())
            {
                try
                {
                    var existingProduct = context.Person.Find(person.Id);

                    if (existingProduct != null)
                    {
                        existingProduct.FirstName = person.FirstName;
                        existingProduct.LastName = person.LastName;

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Person not found!");
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
