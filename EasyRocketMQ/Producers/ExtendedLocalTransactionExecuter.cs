﻿using ons;
using System;

namespace EasyRocketMQ.Producers
{
    /// <summary>
    /// 扩展的事务消息执行者
    /// </summary>
    public class ExtendedLocalTransactionExecuter : LocalTransactionExecuter
    {
        /// <summary>
        /// 业务逻辑方法
        /// </summary>
        private readonly Func<Message, bool> bizFunc;

        /// <summary>
        /// 异常处理方法
        /// </summary>
        private readonly Action<Message, Exception> exceptionAction;

        public ExtendedLocalTransactionExecuter(Func<Message, bool> bizFunc, Action<Message, Exception> exceptionAction = null)
        {
            this.bizFunc = bizFunc;
            this.exceptionAction = exceptionAction;
        }

        public override TransactionStatus execute(Message msg)
        {
            // 消息体内容进行crc32, 也可以使用其它的如MD5
            // 消息ID和crc32id主要是用来防止消息重复
            // 如果业务本身是幂等的, 可以忽略, 否则需要利用msgId或crc32Id来做幂等
            // 如果要求消息绝对不重复, 推荐做法是对消息体body使用crc32或md5来防止重复消息.
            bool success = bizFunc.Invoke(msg);
            if (success)
            {
                // 本地事务成功、提交消息
                return TransactionStatus.CommitTransaction;
            }
            else
            {
                // 本地事务失败、回滚消息
                return TransactionStatus.RollbackTransaction;
            }
        }
    }
}