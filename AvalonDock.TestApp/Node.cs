﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEA3
{
    public class Node
    {
        public Node()
        {
            this.NodeId = Guid.NewGuid().ToString();
            this.IsDeleted = false;
            this.Nodes = new List<Node>();
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        ///  节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 节点携带的内容
        /// </summary>
        public string NodeContent { get; set; }

        /// <summary>
        /// 被删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeType nodetype { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<Node> Nodes { get; set; }
         
    }
    public enum NodeType
    {
        RootNode,//根节点
        DeaNode,
        ComNode,
        DeviceNode
    }
}
