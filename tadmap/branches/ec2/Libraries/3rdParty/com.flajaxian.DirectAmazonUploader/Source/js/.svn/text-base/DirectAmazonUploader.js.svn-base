if (typeof (Flajaxian) == "undefined") Flajaxian = {};
Flajaxian.DirectAmazonUploader = function() {
    this._errorHandlers = [];
}
Flajaxian.DirectAmazonUploader.prototype = {
    set_url: function(value){ this._url = value; },
    get_url: function(){ return this._url; },
    set_path: function(value){ this._path = value; },
    get_path: function(){ return this._path; },
    set_alc: function(value){ this._alc = value; },
    get_alc: function(){ return this._alc; },
    set_uuid: function(value){ this._uuid = value; },
    get_uuid: function(){ return this._uuid; },
    set_awsid: function(value){ this._awsid = value; },
    get_awsid: function(){ return this._awsid; },
    set_confirmFunc: function(value){ this._confirmFunc = value; },
    get_confirmFunc: function(){ return this._confirmFunc; },
    getFileData: function(uploader, array, e){
        this._uploader = uploader;
        var xml = this.getFilesXml(uploader);
        this._req = this.getHttpRequest();
        if(!!this._req){
            e.cancel = true;
            this._req.open("POST", this._url+"&__T=DirAmz.Initial", true);    
            this._req.setRequestHeader("Content-type", "application/x-www-form-urlencoded");    
            this._req.onreadystatechange = Flajaxian.bind(this._onInitialCallResponse, this);
            this._req.send("data="+xml);
        }
    },
    toAttr: function(str){
        return encodeURIComponent(str.split('&').join('&amp;').split('"').join('&quot;'));
    },
    getFilesXml: function(uploader){
        var sb = ["|fs p=\""+this.toAttr(this._path)+"\">"];
        var files = uploader.get_filesList();
        for(var i = 0; i < files.length; i++){
            var f = files[i];
            if(f.state == 1) sb.push("|f i=\""+f.id+"\" n=\""+this.toAttr(f.name)+"\" b=\""+f.bytes+"\" />");
        }
        sb.push("|/fs>");
        return sb.join("");
    },
    stateChange: function(uploader, file, httpStatus, isLast){
        if(file.state > 2){
            var req = this.getHttpRequest();
            if(!!req){
                var sb = [this._url];
                sb.push("__T=DirAmz.Confirm&e=");
                sb.push((file.state == 3)?0:1);
                sb.push("&s=");
                sb.push(httpStatus);
                sb.push("&b=");
                sb.push(file.bytes);
                sb.push("&n=");
                sb.push(encodeURIComponent(file.name));
                if(!!file.changedName){
                    sb.push("&cn=");
                    sb.push(encodeURIComponent(file.changedName));
                }
                sb.push("&l=");
                sb.push((isLast?1:0));
                sb.push("&i=");
                sb.push(file.id);
                sb.push("&r=");
                sb.push((Math.random()+""+(new Date()).getTime())); 
                if(!!this._confirmFunc){
                    req.onreadystatechange = Flajaxian.bind(this._confirmFuncInternal, this);
                    this._req = req;
                }
                req.open("GET", sb.join(""), true);                
                req.send(null);
            }
        }
    },
    getHttpRequest: function(){
        var req = null;
        if(typeof XMLHttpRequest!='undefined'){ try{ req = new XMLHttpRequest(); }catch(e1){ req = null; } }
        if(!req){ try{ req = new ActiveXObject("Msxml2.XMLHTTP"); }catch(e2){ try{ req = new ActiveXObject("Microsoft.XMLHTTP"); }catch(e3){ req = null; } } }
        if (!req && window.createRequest) { try{ req = window.createRequest(); }catch(e4){ req = null; } }
        return req;
    },
    addErrorHandler: function(handler){
        this._errorHandlers.push(handler);
    },
    getLastRequestText: function(){
        return (!this._req) ? null : this._req.responseText;
    },
    _confirmFuncInternal: function(){
        if(this._req.readyState == 4){
            var r = null;
            var text = this.getLastRequestText();
            var start = text.indexOf('{');
            var end = text.lastIndexOf('}')+1;
            if(start >= 0 && end >= 0){
                try{ r = eval("["+text.substr(start, end)+"]")[0]; }catch(e){ r = {}; }
            }
            this._confirmFunc(r);
        }
    },
    _onInitialCallResponse: function(){
        if(this._req.readyState == 4){
            if(this._req.status == 200){
                this._processRequest(this.getLastRequestText());
                this._uploader.setStateVariablesList(this._arr, true);
            }else{
                var e = {cancelUpload:true}
                this._error(e);
                if(e.cancelUpload) this._uploader.cancel();
            }
        }
    },
    _processRequest: function(json){
        var files = eval("["+json+"]")[0];
        this._arr = [];
        for(var j = 0; j < files.length; j++){
            var f = files[j];
            var fi = this._uploader.getFileInfoByID(f.i);
            var keyParts = f.k.split("/");
            var name = keyParts[keyParts.length - 1];
            if(name != fi.name){ fi.changedName = name; }            
            this._arr.push({key:"key", value:f.k, fileID:f.i});
            this._arr.push({key:"Policy", value:f.p, fileID:f.i});
            this._arr.push({key:"Signature", value:f.s, fileID:f.i});
            this._arr.push({key:"Content-Type", value:f.t, fileID:f.i});
        }
        this._arr.push({key:"acl", value:this._alc, fileID:0});
        this._arr.push({key:"x-amz-meta-uuid", value:this._uuid, fileID:0});
        this._arr.push({key:"AWSAccessKeyId", value:this._awsid, fileID:0});
    },
    _error: function(e){
        for(var i = 0; i < this._errorHandlers.length; i++) this._errorHandlers[i](e);
    },
    _addPath: function(name){
        return this._path + name;
    }
}