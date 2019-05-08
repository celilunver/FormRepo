using AutoMapper;
using Lena.Data;
using Lena.Data.UnitofWork;
using Lena.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lena.Business
{
    public interface IUserService : IServiceBase<UserDTO>
    {
        UserDTO getUserWithUsername(string username);
        bool login(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly IUnitofWork uow;
        public UserService(IUnitofWork _uow)
        {
            uow = _uow;
        }
        public UserService()
        {
            uow = new UnitofWork(new LenaDbEntities());
        }

        public UserDTO add(UserDTO entity)
        {
            if(!string.IsNullOrEmpty(entity.Username) && !string.IsNullOrEmpty(entity.Password))
            {
                if(uow.GetRepository<User>().Get(x=>x.Username == entity.Username) == null)
                {
                    User user = Mapper.Map<User>(entity);
                    uow.GetRepository<User>().Add(user);
                    uow.SaveChanges();

                    return Mapper.Map<UserDTO>(user);
                }
            }
            return null;
        }

        public bool delete(int entityId)
        {
            var entity = uow.GetRepository<User>().Get(x => x.Id == entityId);
            if (entity != null)
                entity.IsActive = false;
            
            return uow.SaveChanges() > 0;

        }

        public UserDTO get(int entityId)
        {
            var entity = uow.GetRepository<User>().Get(x => x.Id == entityId);
            if (entity != null)
                return Mapper.Map<UserDTO>(entity);
            return null;
        }

        public List<UserDTO> getAll()
        {
            var entities = uow.GetRepository<User>().GetAll();
            return Mapper.Map<List<UserDTO>>(entities.ToList());
        }

        public bool login(string username, string password)
        {
            var entity = uow.GetRepository<User>().Get(x => x.Username == username && x.Password == password && x.IsActive);
            return entity != null;
        }

        public UserDTO update(UserDTO entity)
        {
            var user = uow.GetRepository<User>().Get(x => x.Id == entity.Id);
            if (user != null)
            {
                user = Mapper.Map<User>(entity);
                uow.SaveChanges();
                return Mapper.Map<UserDTO>(user);
            }
            return null;
        }

        public UserDTO getUserWithUsername(string username)
        {
            var entity = uow.GetRepository<User>().Get(x => x.Username == username);
            if (entity != null)
                return Mapper.Map<UserDTO>(entity);
            return null;
        }
    }
}
