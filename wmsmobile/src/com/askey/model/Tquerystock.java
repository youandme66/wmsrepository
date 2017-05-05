package com.askey.model;

public class Tquerystock {
	private String itemname;
	private String subinv;
	private String frame;
	private String dc;
	private String onhandqty_dc;
	private String onhandqty;
	private String simqty;
	private String pickedqty;
	private String returnflag;
	private String reinspectflag;
	private String lastreinspecttime;
	private String createtime;
	//
	public void Setitemname(String itemname) {
		this.itemname = itemname;
	}

	public String Getitemname() {
		return this.itemname;
	}
	//
	public void Setsubinv(String subinv) {
		this.subinv = subinv;
	}

	public String Getsubinv() {
		return this.subinv;
	}
	//
	public void Setframe(String frame) {
		this.frame = frame;
	}

	public String Getframe() {
		return this.frame;
	}
	//
	public void Setdc(String dc) {
		this.dc = dc;
	}

	public String Getdc() {
		return this.dc;
	}
	//
	public String getOnhandqty_dc() {
		return onhandqty_dc;
	}

	public void setOnhandqty_dc(String onhandqty_dc) {
		this.onhandqty_dc = onhandqty_dc;
	}
	//
	public void Setonhandqty(String onhandqty) {
		this.onhandqty = onhandqty;
	}

	public String Getonhandqty() {
		return this.onhandqty;
	}
	//
	public void Setsimqty(String simqty) {
		this.simqty = simqty;
	}

	public String Getsimqty() {
		return this.simqty;
	}
	//
	public void Setpickedqty(String pickedqty) {
		this.pickedqty = pickedqty;
	}

	public String Getpickedqty() {
		return this.pickedqty;
	}
	//
	public void Setreturnflag(String returnflag) {
		this.returnflag = returnflag;
	}

	public String Getreturnflag() {
		return this.returnflag;
	}
	//
	public void Setreinspectflag(String reinspectflag) {
		this.reinspectflag = reinspectflag;
	}

	public String Getreinspectflag() {
		return this.reinspectflag;
	}
	//
	public void Setlastreinspecttime(String lastreinspecttime) {
		this.lastreinspecttime = lastreinspecttime;
	}

	public String Getlastreinspecttime() {
		return this.lastreinspecttime;
	}
	//
	public void Setcreatetime(String createtime) {
		this.createtime = createtime;
	}

	public String Getcreatetime() {
		return this.createtime;
	}
}
