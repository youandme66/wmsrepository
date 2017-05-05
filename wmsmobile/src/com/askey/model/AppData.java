package com.askey.model;

import java.util.ArrayList;
import java.util.List;

public class AppData {
	public static List<String> mreelid=new ArrayList<String>();
	//
	public static List<Tchangedcframedetail> mchangedcframedetail = new ArrayList<Tchangedcframedetail>();
	public static List<Tchooseorg> mchooseorg = new ArrayList<Tchooseorg>();
	public static List<Tquerystock> mquerystock = new ArrayList<Tquerystock>();
	public static List<Tporcvnoquery> mporcvnoquery = new ArrayList<Tporcvnoquery>();
	public static List<Texchangequery> mexchangequery = new ArrayList<Texchangequery>();
	public static List<Treturnquery> mreturnquery = new ArrayList<Treturnquery>();
	public static List<Tissuequery> missuequery = new ArrayList<Tissuequery>();
	public static List<Tsimlutebasedata> msimlutebasedata = new ArrayList<Tsimlutebasedata>();
	public static List<Tsimulatedata> msimlutedata = new ArrayList<Tsimulatedata>();
	public static List<Tpickedupdetail> mpickedupdetail = new ArrayList<Tpickedupdetail>();
	public static List<Ttakestockquery> mtakestockdetail = new ArrayList<Ttakestockquery>();
	public static List<Twipturninquery> mwipturninquery = new ArrayList<Twipturninquery>();
	public static List<Tpostatusquery> mpostatusquery = new ArrayList<Tpostatusquery>();
	public static List<T6in1> m6in1 = new ArrayList<T6in1>();

	//public static List<String> mBarcodescanItems = new ArrayList<String>();
	//
	public static String scandata="";
	//
	public static String orgid = "";
	public static String orgcode = "";
	public static String user_name = "";
	public static String user_id = "";
	//
	public static String mReception = "";
	public static String ginvoice = "";
	//
	public static String pn_5in1 = "";
	public static String qty_5in1 = "";
	public static String dc_5in1 = "";
	public static String lotno_5in1="";
	public static String reel_5in1="";
	public static String vc_5in1="";
	//
	public static boolean EmpnoIsValid=false;
	public static String VN="";
	public static String PNDesc="";
	public static String BoxID_1="";
	public static String BoxID_2="";
	//
	public static String getallframe="";
	public static String getAframe="";
	public static String getonhandqty="";
	//
	public static String dialog_ret="";
	public static String dialogresult="";
	
	public final static String dialogresult_yes="YES";
	public final static String dialogresult_no="NO";
	//
	public static class colwidth {
		public static Double corgcode = 0.1;
		public static Double corgid = 0.1;
		public static Double cuniqueid = 0.2;
		public static Double citemname = 0.4;
		public static Double csubinv = 0.1;
		//public static Double csubinv_out = 0.2;
		//public static Double csubinv_in = 0.2;
		public static Double cwo=0.4;
		public static Double copseq=0.1;
		public static Double creasonname=0.3;

		public static Double cqty = 0.2;
		public static Double cdc = 0.2;
		public static Double cframe = 0.2;
		public static Double cinvoice = 0.4;
		public static Double ctranstype = 0.5;
		public static Double clocator = 0.2;
		public static Double cregionname = 0.2;
        public static Double ctime=0.4;
		public static Double cdept = 0.3;
		public static Double cpo = 0.2;
		public static Double cpoline = 0.1;
		public static Double ciqcflag = 0.2;
		public static Double creturnflag = 0.25;
		public static Double creinspectflag = 0.25;
		public static Double ctxnflag=0.18;
		public static Double ctxntype=0.4;
		public static Double cusername=0.2;
		public static Double cversion=0.1;
		
	}

	//
	public static int screenwidth = 150;
}
