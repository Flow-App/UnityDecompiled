﻿// Decompiled with JetBrains decompiler
// Type: UnityEditor.Modules.IBuildAnalyzer
// Assembly: UnityEditor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 53BAA40C-AA1D-48D3-AA10-3FCF36D212BC
// Assembly location: C:\Program Files\Unity 5\Editor\Data\Managed\UnityEditor.dll

using UnityEditor.BuildReporting;

namespace UnityEditor.Modules
{
  internal interface IBuildAnalyzer
  {
    void OnAddedExecutable(BuildReport report, int fileIndex);
  }
}