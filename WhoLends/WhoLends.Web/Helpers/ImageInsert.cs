using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using AutoMapper;
using WhoLends.Data;
using WhoLends.ViewModels;
using WhoLends.Web.DAL;
using WhoLends.Web.DAL.Implementations;
using File = WhoLends.Data.File;

namespace WhoLends.Web.Helpers
{
    public static class ImageInsert
    {
        private static IFileRepository _fileRepository;

        private static void InitiateAutoMapper()
        {
            _fileRepository = new FileRepository(new Entities());

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>();
                cfg.CreateMap<LendItem, LendItemViewModel>().ReverseMap();
                cfg.CreateMap<Data.File, FileViewModel>().ReverseMap();
            });
        }

        public static List<FileViewModel> InsertImages(HttpPostedFileBase uploadfile, int targetObjectId)
        {
            InitiateAutoMapper();

            //process Attached Images
            if (uploadfile != null && uploadfile.ContentLength > 0)
            {
                FileViewModel fileVM = new FileViewModel();
                using (var reader = new BinaryReader(uploadfile.InputStream))
                {
                    fileVM.Content = reader.ReadBytes(uploadfile.ContentLength);
                }

                fileVM.FileName = uploadfile.FileName;
                fileVM.LendItemId = targetObjectId;

                //add file to DB
                var filemodel = Mapper.Map<FileViewModel, File>(fileVM);
                _fileRepository.InsertFile(filemodel);
                _fileRepository.Save();

                //adding to LendItem VM
                List<FileViewModel> list = new List<FileViewModel>();
                list.Add(fileVM);

                return list;
            }
            return null;
        }


    }
}