package com.askey.model;

import android.app.Application;


public class K_table {
	/*
	 * public static String APIURL =
	 * "http://b2b.askey.cn/appservice/ServiceLibrary.asmx/"; public static
	 * String LoginURL =
	 * "http://b2b.askey.cn/appservice/ServiceLibrary.asmx/ValidateUser1?UserName=%s&UserPwd=%s"
	 * ; public static String GetTodolistURL111 =
	 * "http://b2b.askey.cn/appservice/ServiceLibrary.asmx/GetToDoList4Android?UserAD=%s"
	 * ;
	 */
	//webservice地址	
	//WJ正式庫
	//static String APServerHead ="http://10.7.46.169/WMSService_Android/WebService_Android.asmx/";
	
	//WJ測試庫
	static String APServerHead = "http://121.41.45.122/PDA/PrintHandler.ashx";
	
	//TW WMS
	//static String APServerHead ="http://10.1.6.192/WebService_android.asmx/";
	//
	public static String GetIOMTLURL = APServerHead
			+ "QueryIOMTL?PN=%s&subinv=%s&orgid=%s";
	public static String updatedcframe = APServerHead
			+ "Updatedcframe?unqid=%s&subinv=%s&onhandqty=%s&simqty=%s&dc=%s&frame=%s";
	public static String chooseorg = APServerHead + "QueryORG?userid=%s";
	public static String querystock = APServerHead
			+ "QueryStock?PN=%s&dc=%s&subinv=%s&orgid=%s";
	public static String poqueryrcvno = APServerHead
			+ "POQueryRcvNO?rcvno=%s&orgid=%s";
	public static String podelivertrans = APServerHead
			+ "PODeliverTrans?rcvno=%s"
			+ "&po_no=%s&orgid=%s&orgcode=%s&item_name=%s&datecode=%s&locator=%s&temp_region=%s&frame=%s&temp_qty=%s&rcv_qty=%s"
			+ "&delivered_qty=%s&iqc_flag=%s&po_header_id=%s&po_line_id=%s&line_location_id=%s&distribution_id=%s&invoice=%s"
			+ "&special_no=%s&special_wo_no=%s&user_id=%s&suffocate_code=%s";
	public static String exchangequery = APServerHead
			+ "ExchangeQuery?invoice_no=%s";
	public static String exchangequery_in = APServerHead
			+ "ExchangeQuery_in?invoice_no=%s";
	public static String exchangetrans = APServerHead
			+ "ExchangeTrans?exchange_header_id=%s"
			+ "&exchange_line_id=%s&invoice_no=%s&item_name=%s&item_id=%s&required_qty=%s&balance_qty=%s&out_sub_name=%s"
			+ "&out_locator_name=%s&out_org_id=%s&in_sub_name=%s&in_locator_name=%s&in_org_id=%s&in_frame_name=%s"
			+ "&out_frame_name=%s&model_name=%s&duty_dept_code=%s&scrap_type=%s&model_type=%s&reason_code=%s"
			+ "&wo_no=%s&scrap_line_id=%s&remark=%s&customer_code=%s&demand_id=%s&suffocate_code=%s&transaction_type_id=%s"
			+ "&charge_no=%s&create_by=%s&qty=%s&dc=%s";

	public static String exchangetrans_out = APServerHead
			+ "Exchange_out?exchange_header_id=%s"
			+ "&exchange_line_id=%s&invoice_no=%s&item_name=%s&item_id=%s&required_qty=%s&balance_qty=%s&out_sub_name=%s"
			+ "&in_sub_name=%s&in_org_id=%s&out_locator_name=%s&out_org_id=%s"
			+ "&out_frame_name=%s&model_name=%s&duty_dept_code=%s&scrap_type=%s&model_type=%s&reason_code=%s"
			+ "&wo_no=%s&scrap_line_id=%s&remark=%s&customer_code=%s&demand_id=%s&suffocate_code=%s&transaction_type_id=%s"
			+ "&charge_no=%s&create_by=%s&qty=%s&dc=%s";

	public static String exchangetrans_in = APServerHead
			+ "Exchange_in?"
			+ "exchange_line_id=%s&invoice_no=%s&item_name=%s&item_id=%s"
			+ "&out_sub_name=%s&out_locator_name=%s&out_org_id=%s&out_frame_name=%s"
			+ "&in_sub_name=%s&in_locator_name=%s&in_org_id=%s&in_frame_name=%s"
			+ "&model_name=%s&duty_dept_code=%s&scrap_type=%s&model_type=%s&reason_code=%s"
			+ "&wo_no=%s&scrap_line_id=%s&remark=%s&customer_code=%s&demand_id=%s&suffocate_code=%s"
			+ "&transaction_type_id=%s"
			+ "&charge_no=%s&create_by=%s&v_in_qty=%s&dc=%s&operation_line_id=%s&out_mtl_io_id=%s";

	public static String returnquery = APServerHead
			+ "ReturnQuery?invoice_no=%s";
	public static String returntrans = APServerHead
			+ "ReturnTrans?org_id=%s"
			+ "&invoice_no=%s&wo_no=%s&wo_key=%s&route=%s&reason_code=%s&item_name=%s&item_id=%s"
			+ "&required_qty=%s&balance_qty=%s&sub_name=%s&locator=%s&frame=%s&dc=%s&qty=%s&pu=%s"
			+ "&reason_id=%s&dept_code=%s&remark=%s&batch_line_id=%s&create_by=%s&transaction_type_id=%s"
			+ "&return_line_id=%s&suffocate_code=%s";
	public static String issuequery = APServerHead + "IssueQuery?invoice_no=%s";
	public static String issuetrans = APServerHead
			+ "IssueTrans?org_id=%s"
			+ "&invoice_no=%s&wo=%s&wip_entity_id=%s&operation_seq_num=%s&reason_name=%s&item_name=%s"
			+ "&item_id=%s&required_qty=%s&balance_qty=%s&sub_name=%s&locator=%s&frame=%s&dc=%s"
			+ "&qty=%s&pu=%s&reason_id=%s&dept_code=%s&transaction_type_id=%s&suffocate_code=%s&remark=%s"
			+ "&model_name=%s&customer_code=%s&demand_id=%s&batch_line_id=%s&issue_line_id=%s&create_by=%s";
	public static String userquery = APServerHead
			+ "QueryUser?user_name=%s&password=%s";
	public static String SimulateTrans=APServerHead
			+"SimulateIssueMtlTrans?invoice_no=%s&org_id=%s&user_id=%s";
	//
	public static String InsertSpitData = APServerHead
			+ "spit6in1label?PN=%s&LotNo=%s&DC=%s&VC=%s&BoxID_old=%s&BoxID_1=%s&BoxID_2=%s&qty1=%s&qty2=%s&userID=%s&empno=%s";
	public static String CheckEmpno = APServerHead + "CheckEmp?empno=%s";
	public static String GetVNByVC = APServerHead + "QueryVNByVC?vc=%s";
	public static String QueryDescByPN = APServerHead + "QueryDescByPN?pn=%s";
	public static String Get6in1ID = APServerHead + "Get6in1ID?";
	//
	public static String GetInvoicHeadQuery = APServerHead
			+ "getInvoiceHead?type=%s&orgcode=%s";
	public static String getallframe = APServerHead
			+ "getallframe?orgcode=%s&subname=%s&itemname=%s";
	public static String getonhandqty = APServerHead
			+ "getonhandqty?orgid=%s&subname=%s&itemname=%s";
	public static String inserttakestockdata = APServerHead
			+ "InsertTakeStockData?org_id=%s&item_name=%s&sub_code=%s"
			+ "&onhandqty=%s&scanedqty=%s&create_by=%s";
	public static String getsimbasedata = APServerHead
			+ "GetSimulateBaseData?orgid=%s&simulateid=%s";
	public static String getsimdata = APServerHead
			+ "GetSimulateData?orgid=%s&simulateid=%s&querytype=%s&opseq=%s"
			+ "&subname=%s&itemname=%s";
	public static String dopickup = APServerHead
			+ "dopickup_checkreel?orgid=%s&simulateid=%s&opseq=%s&subname=%s&itemname=%s"
			+ "&qty=%s&dc=%s&reelid=%s&vendorcode=%s&userid=%s";
	public static String dopickup_merger = APServerHead
			+ "dopickup_checkreel?orgid=%s&simulateid=%s&subname=%s&itemname=%s"
			+ "&qty=%s&dc=%s&reelid=%s&vendorcode=%s&userid=%s";
	public static String getpickedupdetail = APServerHead
			+ "GetPickedupdetail?simulateid=%s&itemname=%s";
	public static String gettakestockdetail = APServerHead
			+ "takestockquery?orgid=%s&subname=%s&itemname=%s&querytype=%s&totalqty=%s";
	public static String GetPickingupQty=APServerHead
			+"GetPickingupQty?orgid=%s&pn=%s&subname=%s";
	public static String wipturninquery = APServerHead+"WipTurninQueryRcvNO?invoice_no=%s&org_id=%s";
	
	public static String wipturnincommit = APServerHead+"WipTurninTrans?unique_id=%s"
			+ "&invoice_no=%s&wo_no=%s&region_name=%s&part_no=%s&required_qty=%s"
			+ "&balance_qty=%s&subinventory_name=%s"
			+ "&locator_name=%s&version=%s&inputqty=%s&frame_name=%s"
			+ "&user_id=%s&org_id=%s";
	public static String queryPoStatusByReceiptNo=APServerHead+"queryPoStatusByReceiptNo?"
			+"ReceiptNo=%s&orgid=%s";
	public static String poMergerLineScanCommitdata=APServerHead+"poMergerLineScanCommitdata?"
			+"prefix=%s&deliverno_suffix=%s&pn=%s&lotno=%s&datecode=%s"
			+"&boxno=%s&vendorcode=%s&qty=%s&userid=%s&orgcode=%s";
	public static String checkScanedData=APServerHead+"checkScanedData?prefix=%s&"
			+"deliverno_suffix=%s&pn=%s&vc=%s&reelid=%s&orgcode=%s";
	public static String checkASNAndPO=APServerHead+"checkASNAndPO?shipno=%s&po=%s"
			+"&cartonno=%s&ASN=%s&qty1=%s&qty2=%s";
	public static String finishproductturnin=APServerHead
			+"Turnintrans?barcode=%s&user_id=%s&org_id=%s";
	//
	public static String excuteres = "";

}