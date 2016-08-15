using AutoMapper;
using Domain;
using MyData.BLL.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Domain.Memory;

namespace MyData.Models
{
    public class MapperConfig
    {
        public static void Init()
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<CTask, TaskRowVM>()
                      .ForMember(x => x.Deadline, o => o.MapFrom(y => y.Deadline.ToDate()));

                    cfg.CreateMap<CIteration, IterationRowVM>();
                    cfg.CreateMap<CIteration, IterationVM>();

                    cfg.CreateMap<CProject, ProjectVM>();
                    cfg.CreateMap<CProject, ProjectRowVM>()
                        .ForMember(x => x.Deadline, o => o.MapFrom(y => y.Deadline.ToDate()));

                    cfg.CreateMap<CSet, SetRowVM>();
                    cfg.CreateMap<CSet, SetEditVM>();

                    cfg.CreateMap<CTerm, TermRowVM>();
                    cfg.CreateMap<CTerm, TermEditVM>();

                    cfg.CreateMap<CFolder, FolderVM>();
                    cfg.CreateMap<CFolder, FolderRowVM>();
                    cfg.CreateMap<CFolder, FolderEditVM>();
                });

                var tasks = new ManagerSite().Tasks.GetTasks();
                var item = Mapper.Map<List<TaskRowVM>>(tasks);
            }
            catch (Exception ex)
            {

            }
        }
    }
}