//*************************************************************************
//	创建日期:	2016-8-22
//	文件名称:	NetConnect.cs
//  创 建 人:   Silekey
//	版权所有:	LT
//	说    明:	网络错误类
//*************************************************************************

using UnityEngine;
using System.Collections;

namespace GameNet {
	public enum NET_ERR : int {
		SUCCESS = 0,
		BUFFFULL = 1,
		ALREADY_REGISTER = 2,
		CONN_DNS_FAIL = 3,
		BUFF_LESS = 4, // BUFF 不够

		ARGUMENT_FAIL, // 参数错误
		SOCKET_CLOSE,  // socket已经关闭
        OUT_OF_MEMORY, // 内存不够！
        FAULT,         // 内部状态错误！
		UNKNOWN,
		SOCKET_ERR
	}
}
