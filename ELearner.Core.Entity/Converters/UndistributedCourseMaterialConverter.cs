using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Converters {
    public class UndistributedCourseMaterialConverter {

        public UndistributedCourseMaterial Convert(UndistributedCourseMaterialBO material) {
            var converted = new UndistributedCourseMaterial() {
                Id = material.Id,
                VideoId = material.VideoId,
                CourseId = material.CourseId
            };
            return converted;
        }

        public UndistributedCourseMaterialBO Convert(UndistributedCourseMaterial material) {
            var converted = new UndistributedCourseMaterialBO() {
                Id = material.Id,
                VideoId = material.VideoId,
                CourseId = material.CourseId
            };
            return converted;
        }
    }
}
