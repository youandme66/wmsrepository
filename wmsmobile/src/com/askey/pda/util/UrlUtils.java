package com.askey.pda.util;

import java.util.Iterator;
import java.util.Map;

/**
 * Created by gym on 2016/10/21.
 */
public class UrlUtils {
    public static final String BASE_URL ="http://121.41.45.122/PDA/";
    public static final String PRINT_DETAIL ="insert_wms_6in1_detail.ashx";
    public static final String PRINT_RELATION ="insert_wms_6in1_id_relation.ashx";
    
    public static final int DETAIL_TABLE =1;
    public static final int RELATION_TABLE =2;

    public static String getUrl(String index){
        return BASE_URL + index;
    }

    public static String getPrintUrl(Map<String,String> printData, int type){
    	StringBuilder urlBuilder = null;
    	
    	if(type == DETAIL_TABLE){
    		urlBuilder = new StringBuilder(BASE_URL + PRINT_DETAIL).append("?");
    	}else if(type == RELATION_TABLE){
    		urlBuilder = new StringBuilder(BASE_URL + PRINT_RELATION).append("?");
    	}
    	
    	//轮询map，取出数据
    	Iterator<Map.Entry<String, String>> it = printData.entrySet().iterator();    
    	while (it.hasNext()) {    
    	    Map.Entry<String, String> entry = it.next(); 
    	    urlBuilder.append(entry.getKey()).append("=").append(entry.getValue()).append("&");
    	}
    	
    	//最后返回的url需要去掉最后一个多余的  &
		return urlBuilder.substring(0, urlBuilder.length() - 1);
    }
}
