﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Core.Entity;

namespace RayPI.Infrastructure.Repository.Core.Context
{
    /// <summary>Map实体基类的基础字段</summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityBaseTypeConfig<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(it => it.Id).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.CreateId).IsRequired(false);
            builder.Property(x => x.CreateName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.CreateTime).IsRequired(false);
            builder.Property(x => x.UpdateId).IsRequired(false);
            builder.Property(x => x.UpdateName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.UpdateTime).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Ignore(x => x.AutoSetter);
        }
    }
}
