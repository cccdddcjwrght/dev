using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZEditors;

[ZEditorSymbol("use_sdk",symbol = "USE_THIRD_SDK",title = "激活三方sdk")]
[ZEditorSymbol("td_sdk", symbol = "TD_OFF", title = "关闭数数")]
[ZEditorSymbol("ad_sdk", symbol = "USE_AD", title = "开启广告")]
[ZEditor("SDK")]
public class SDKDefine :ZEditors.IZEditor
{

}
