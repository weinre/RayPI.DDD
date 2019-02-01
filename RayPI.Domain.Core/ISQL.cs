using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RayPI.Domain.Core
{
    public interface ISQL
    {
        /// <summary>执行普通查询</summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> ExecuteQuery<TAggregateRoot>(string sqlQuery, params object[] parameters);

        /// <summary>执行sql语句</summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);

        /// <summary>执行存储过程</summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        IEnumerable<TAggregateRoot> ExecuteStoredProcedure<TAggregateRoot>(object storedProcedure);

        /// <summary>执行无返回值的存储过程</summary>
        /// <param name="storedProcedure"></param>
        void ExecuteStoredProcedure(object storedProcedure);

        /// <summary>开始事务</summary>
        /// <returns></returns>
        void BeginTrans(IsolationLevel level = IsolationLevel.ReadCommitted);

        /// <summary>事务回滚</summary>
        void TransRollBack();

        /// <summary>提交事务</summary>
        void TransCommit();
    }
}
