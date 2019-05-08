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
    public interface IFieldService : IServiceBase<FieldDTO>
    {
        List<FieldDTO> getFieldsByFormId(int formId);
    }
    public class FieldService : IFieldService
    {
        private readonly IUnitofWork uow;
        public FieldService(IUnitofWork _uow)
        {
            uow = _uow;
        }
        public FieldService()
        {
            uow = new UnitofWork(new LenaDbEntities2());
        }

        public FieldDTO add(FieldDTO entity)
        {
            if (entity.FormId > 0 && !string.IsNullOrEmpty(entity.Name) && !string.IsNullOrEmpty(entity.DataType))
            {
                    Field field = Mapper.Map<Field>(entity);
                    uow.GetRepository<Field>().Add(field);
                    uow.SaveChanges();

                    return Mapper.Map<FieldDTO>(field);
            }
            return null;
        }
        public bool delete(int entityId)
        {
            var entity = uow.GetRepository<Field>().Get(x => x.Id == entityId);
            if (entity != null)
            {
                entity.IsActive = false;
                return uow.SaveChanges() > 0;
            }
            return false;
        }

        public FieldDTO get(int entityId)
        {
            var entity = uow.GetRepository<Field>().Get(x => x.Id == entityId);
            if (entity != null)
                return Mapper.Map<FieldDTO>(entity);
            return null;
        }

        public List<FieldDTO> getAll()
        {
            var entities = uow.GetRepository<Field>().GetAll();
            return Mapper.Map<List<FieldDTO>>(entities.ToList());
        }

        public List<FieldDTO> getFieldsByFormId(int formId)
        {
            var entities = uow.GetRepository<Field>().Get(x => x.FormId == formId);
            return Mapper.Map<List<FieldDTO>>(entities);
        }

        public FieldDTO update(FieldDTO entity)
        {
            var field = uow.GetRepository<Field>().Get(x => x.Id == entity.Id);
            if (field != null)
            {
                field = Mapper.Map<Field>(entity);
                uow.SaveChanges();
                return Mapper.Map<FieldDTO>(field);
            }
            return null;
        }

    }
}
