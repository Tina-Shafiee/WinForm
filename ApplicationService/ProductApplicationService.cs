

using ApplicationService.Dtos;
using Model.DomainModels;
using Model.ServiceModels;

namespace ApplicationService
{
    public class ProductApplicationService
    {
        private readonly ProductServiceModel _productServiceModel;

        public ProductApplicationService()
        {
            _productServiceModel = new ProductServiceModel();
        }

        //Method called GetAllProduct
        public List<GetProductDto> GetAllProduct()
        {
            var p = _productServiceModel.SelectAll();
            var getProductDtos = new List<GetProductDto>();
            foreach (var item in p)
            {
                var getProductDto = new GetProductDto()
                {
                    Id = item.Id,
                    Title = item.Title,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                };
                getProductDtos.Add(getProductDto);
            }
            return getProductDtos;
        }


        public void PostProduct(PostProductDto postProductDto)
        {
            var product = new Product()
            {
                Title = postProductDto.Title,
                UnitPrice = postProductDto.UnitPrice,
                Quantity = postProductDto.Quantity,
            };
            _productServiceModel.InsertProduct(product);
        }


        public void DeleteProduct(DeleteProductDto deleteProductDto)
        {
            var product = new Product()
            {
                Id = deleteProductDto.Id,
            };

            _productServiceModel.Remove(product);
        }

        public void UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = new Product()
            {
                Id = updateProductDto.Id,
                Title = updateProductDto.Title,
                UnitPrice = updateProductDto.UnitPrice,
                Quantity = updateProductDto.Quantity,
            };
            _productServiceModel.Update(product);
        }

        

        
    }
}
