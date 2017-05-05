package com.askey.model;

public class Tchangedcframedetail {
	//item_name,subinventory_name,onhand_qty,simulated_qty,datecode
	private String unique_id;
	private String item_name;
	private String subinventory_name;
	private String onhand_qty;
	private String simulated_qty;
	private String datecode;
	private String frame;
	//
	public void Setunique_id(String unique_id) {
		/*
		if (unique_id.length()<7)
		{
			for (int i=unique_id.length();i<7;i++)
				unique_id=unique_id+" ";
		}
		*/
		this.unique_id = unique_id;
	}

	public String Getunique_id() {
		return this.unique_id;
	}    
	
	public void Setitem_name(String item_name) {
		this.item_name = item_name;
	}

	public String Getitem_name() {
		return this.item_name;
	}
	//
	public void Setsubinventory_name(String subinventory_name) {
		this.subinventory_name = subinventory_name;
	}

	public String Getsubinventory_name() {
		return this.subinventory_name;
	}	
    //
	public void Setonhand_qty(String onhand_qty) {
		this.onhand_qty = onhand_qty;
	}

	public String Getonhand_qty() {
		return this.onhand_qty;
	}
	//
	public String Getsimulated_qty() {
		return this.simulated_qty;
	}
	public void Setsimulated_qty(String simulated_qty) {
		this.simulated_qty = simulated_qty;
	}
	//
	public String Getdatecode() {
		return this.datecode;
	}
	public void Setdatecode(String datecode) {
		this.datecode = datecode;
	}
	//
	public String Getframe() {
		return this.frame;
	}
	public void Setframe(String frame) {
		this.frame = frame;
	}
}
