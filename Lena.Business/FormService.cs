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
    public interface IFormService : IServiceBase<FormDTO>
    {

    }
    public class FormService : IFormService
    {
        private readonly IUnitofWork uow;
        public FormService(IUnitofWork _uow)
        {
            uow = _uow;
        }
        public FormService()
        {
            uow = new UnitofWork(new LenaDbEntities2());
        }
        public FormDTO add(FormDTO entity)
        {
            if (!string.IsNullOrEmpty(entity.Name) && entity.CreatedAt != null)
            {
                Form form = Mapper.Map<Form>(entity);
                uow.GetRepository<Form>().Add(form);
                uow.SaveChanges();

                return Mapper.Map<FormDTO>(form);
            }
            return null;
        }
        public bool delete(int entityId)
        {
            var entity = uow.GetRepository<Form>().Get(x => x.Id == entityId);
            if (entity != null)
            {
                entity.IsActive = false;
                return uow.SaveChanges() > 0;
            }
            return false;
        }

        public FormDTO get(int entityId)
        {
            var entity = uow.GetRepository<Form>().Get(x => x.Id == entityId && x.IsActive);
            if (entity != null)
                return Mapper.Map<FormDTO>(entity);
            return null;
        }

        public List<FormDTO> getAll()
        {
            var entities = uow.GetRepository<Form>().GetAll().Where(x=>x.IsActive);
            return Mapper.Map<List<FormDTO>>(entities.ToList());
        }

        public FormDTO update(FormDTO entity)
        {
            var form = uow.GetRepository<Form>().Get(x => x.Id == entity.Id);
            if (form != null)
            {
                form = Mapper.Map<Form>(entity);
                uow.SaveChanges();
                return Mapper.Map<FormDTO>(form);
            }
            return null;
        }
    }
}
