﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NasleGhalam.WebApi.SmsPanelService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SmsPanelService.FastSendSoap")]
    public interface FastSendSoap {
        
        // CODEGEN: Generating message contract since element name Username from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoSendCode", ReplyAction="*")]
        NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponse AutoSendCode(NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutoSendCode", ReplyAction="*")]
        System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponse> AutoSendCodeAsync(NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest request);
        
        // CODEGEN: Generating message contract since element name Username from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendMessageWithCode", ReplyAction="*")]
        NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponse SendMessageWithCode(NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendMessageWithCode", ReplyAction="*")]
        System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponse> SendMessageWithCodeAsync(NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest request);
        
        // CODEGEN: Generating message contract since element name Username from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CheckSendCode", ReplyAction="*")]
        NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponse CheckSendCode(NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CheckSendCode", ReplyAction="*")]
        System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponse> CheckSendCodeAsync(NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AutoSendCodeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AutoSendCode", Namespace="http://tempuri.org/", Order=0)]
        public NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequestBody Body;
        
        public AutoSendCodeRequest() {
        }
        
        public AutoSendCodeRequest(NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AutoSendCodeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ReciptionNumber;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Footer;
        
        public AutoSendCodeRequestBody() {
        }
        
        public AutoSendCodeRequestBody(string Username, string Password, string ReciptionNumber, string Footer) {
            this.Username = Username;
            this.Password = Password;
            this.ReciptionNumber = ReciptionNumber;
            this.Footer = Footer;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AutoSendCodeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AutoSendCodeResponse", Namespace="http://tempuri.org/", Order=0)]
        public NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponseBody Body;
        
        public AutoSendCodeResponse() {
        }
        
        public AutoSendCodeResponse(NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AutoSendCodeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long AutoSendCodeResult;
        
        public AutoSendCodeResponseBody() {
        }
        
        public AutoSendCodeResponseBody(long AutoSendCodeResult) {
            this.AutoSendCodeResult = AutoSendCodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendMessageWithCodeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendMessageWithCode", Namespace="http://tempuri.org/", Order=0)]
        public NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequestBody Body;
        
        public SendMessageWithCodeRequest() {
        }
        
        public SendMessageWithCodeRequest(NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendMessageWithCodeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ReciptionNumber;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Code;
        
        public SendMessageWithCodeRequestBody() {
        }
        
        public SendMessageWithCodeRequestBody(string Username, string Password, string ReciptionNumber, string Code) {
            this.Username = Username;
            this.Password = Password;
            this.ReciptionNumber = ReciptionNumber;
            this.Code = Code;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendMessageWithCodeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendMessageWithCodeResponse", Namespace="http://tempuri.org/", Order=0)]
        public NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponseBody Body;
        
        public SendMessageWithCodeResponse() {
        }
        
        public SendMessageWithCodeResponse(NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendMessageWithCodeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long SendMessageWithCodeResult;
        
        public SendMessageWithCodeResponseBody() {
        }
        
        public SendMessageWithCodeResponseBody(long SendMessageWithCodeResult) {
            this.SendMessageWithCodeResult = SendMessageWithCodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CheckSendCodeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CheckSendCode", Namespace="http://tempuri.org/", Order=0)]
        public NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequestBody Body;
        
        public CheckSendCodeRequest() {
        }
        
        public CheckSendCodeRequest(NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CheckSendCodeRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ReciptionNumber;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Code;
        
        public CheckSendCodeRequestBody() {
        }
        
        public CheckSendCodeRequestBody(string Username, string Password, string ReciptionNumber, string Code) {
            this.Username = Username;
            this.Password = Password;
            this.ReciptionNumber = ReciptionNumber;
            this.Code = Code;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CheckSendCodeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CheckSendCodeResponse", Namespace="http://tempuri.org/", Order=0)]
        public NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponseBody Body;
        
        public CheckSendCodeResponse() {
        }
        
        public CheckSendCodeResponse(NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CheckSendCodeResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool CheckSendCodeResult;
        
        public CheckSendCodeResponseBody() {
        }
        
        public CheckSendCodeResponseBody(bool CheckSendCodeResult) {
            this.CheckSendCodeResult = CheckSendCodeResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FastSendSoapChannel : NasleGhalam.WebApi.SmsPanelService.FastSendSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FastSendSoapClient : System.ServiceModel.ClientBase<NasleGhalam.WebApi.SmsPanelService.FastSendSoap>, NasleGhalam.WebApi.SmsPanelService.FastSendSoap {
        
        public FastSendSoapClient() {
        }
        
        public FastSendSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FastSendSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FastSendSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FastSendSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponse NasleGhalam.WebApi.SmsPanelService.FastSendSoap.AutoSendCode(NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest request) {
            return base.Channel.AutoSendCode(request);
        }
        
        public long AutoSendCode(string Username, string Password, string ReciptionNumber, string Footer) {
            NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest inValue = new NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest();
            inValue.Body = new NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.ReciptionNumber = ReciptionNumber;
            inValue.Body.Footer = Footer;
            NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponse retVal = ((NasleGhalam.WebApi.SmsPanelService.FastSendSoap)(this)).AutoSendCode(inValue);
            return retVal.Body.AutoSendCodeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponse> NasleGhalam.WebApi.SmsPanelService.FastSendSoap.AutoSendCodeAsync(NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest request) {
            return base.Channel.AutoSendCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.AutoSendCodeResponse> AutoSendCodeAsync(string Username, string Password, string ReciptionNumber, string Footer) {
            NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest inValue = new NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequest();
            inValue.Body = new NasleGhalam.WebApi.SmsPanelService.AutoSendCodeRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.ReciptionNumber = ReciptionNumber;
            inValue.Body.Footer = Footer;
            return ((NasleGhalam.WebApi.SmsPanelService.FastSendSoap)(this)).AutoSendCodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponse NasleGhalam.WebApi.SmsPanelService.FastSendSoap.SendMessageWithCode(NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest request) {
            return base.Channel.SendMessageWithCode(request);
        }
        
        public long SendMessageWithCode(string Username, string Password, string ReciptionNumber, string Code) {
            NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest inValue = new NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest();
            inValue.Body = new NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.ReciptionNumber = ReciptionNumber;
            inValue.Body.Code = Code;
            NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponse retVal = ((NasleGhalam.WebApi.SmsPanelService.FastSendSoap)(this)).SendMessageWithCode(inValue);
            return retVal.Body.SendMessageWithCodeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponse> NasleGhalam.WebApi.SmsPanelService.FastSendSoap.SendMessageWithCodeAsync(NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest request) {
            return base.Channel.SendMessageWithCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeResponse> SendMessageWithCodeAsync(string Username, string Password, string ReciptionNumber, string Code) {
            NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest inValue = new NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequest();
            inValue.Body = new NasleGhalam.WebApi.SmsPanelService.SendMessageWithCodeRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.ReciptionNumber = ReciptionNumber;
            inValue.Body.Code = Code;
            return ((NasleGhalam.WebApi.SmsPanelService.FastSendSoap)(this)).SendMessageWithCodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponse NasleGhalam.WebApi.SmsPanelService.FastSendSoap.CheckSendCode(NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest request) {
            return base.Channel.CheckSendCode(request);
        }
        
        public bool CheckSendCode(string Username, string Password, string ReciptionNumber, string Code) {
            NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest inValue = new NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest();
            inValue.Body = new NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.ReciptionNumber = ReciptionNumber;
            inValue.Body.Code = Code;
            NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponse retVal = ((NasleGhalam.WebApi.SmsPanelService.FastSendSoap)(this)).CheckSendCode(inValue);
            return retVal.Body.CheckSendCodeResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponse> NasleGhalam.WebApi.SmsPanelService.FastSendSoap.CheckSendCodeAsync(NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest request) {
            return base.Channel.CheckSendCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<NasleGhalam.WebApi.SmsPanelService.CheckSendCodeResponse> CheckSendCodeAsync(string Username, string Password, string ReciptionNumber, string Code) {
            NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest inValue = new NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequest();
            inValue.Body = new NasleGhalam.WebApi.SmsPanelService.CheckSendCodeRequestBody();
            inValue.Body.Username = Username;
            inValue.Body.Password = Password;
            inValue.Body.ReciptionNumber = ReciptionNumber;
            inValue.Body.Code = Code;
            return ((NasleGhalam.WebApi.SmsPanelService.FastSendSoap)(this)).CheckSendCodeAsync(inValue);
        }
    }
}