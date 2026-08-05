using ApplicationService.Dtos;
using Model.DomainModels;
using Model.ServiceModels;


namespace ApplicationService
{
    public class PersonApplicationService
    {
        private readonly PersonServiceModel _personServiceModel;

        public PersonApplicationService()
        {
            _personServiceModel = new PersonServiceModel();
        }

        //Method called GetAllPerson
        public List<GetPersonDto> GetAllPerson()
        {
            var p = _personServiceModel.SelectAll();
            var getPersonDtos = new List<GetPersonDto>();
            foreach (var item in p) 
            {
                var getPersonDto = new GetPersonDto()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                };
                getPersonDtos.Add(getPersonDto);
            }
            return getPersonDtos;  
        }

        #region [- Post() -]
        public void PostPerson(PostPersonDto postPersonDto)
        {
            var person = new Person()
            {
                FirstName = postPersonDto.FirstName,
                LastName = postPersonDto.LastName,
            };
            _personServiceModel.Insert(person);
        }
        #endregion


        public void DeletePerson(DeletePersonDto deletePersonDto)
        {
            var person = new Person()
            { Id = deletePersonDto.Id };

            _personServiceModel.Remove(person);
        }


        public void UpdatePerson(UpdatePersonDto updatePersonDto)
        {
            var person = new Person()
            {
                Id = updatePersonDto.Id,
                FirstName = updatePersonDto.FirstName,
                LastName = updatePersonDto.LastName,
            };
            _personServiceModel.Update(person);
        }


    }
}
