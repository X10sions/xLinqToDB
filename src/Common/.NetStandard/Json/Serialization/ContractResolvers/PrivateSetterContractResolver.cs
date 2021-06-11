﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Common.Json.Serialization.ContractResolvers {
  public class PrivateSetterContractResolver : DefaultContractResolver {

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
      var jProperty = base.CreateProperty(member, memberSerialization);
      if (jProperty.Writable) {
        return jProperty;
      }
      jProperty.Writable = member.IsPropertyWithSetter();
      return jProperty;
    }

  }
}