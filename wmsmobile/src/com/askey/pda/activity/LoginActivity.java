package com.askey.pda.activity;

import com.askey.pda.util.UrlUtils;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.TextView;


public class LoginActivity extends Activity{

    private Button btnLogin;
    private TextView tvForgertPwd;
    private TextView tvRegister;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_login);
        
//        initView();

        String url = UrlUtils.getUrl("LoginPDA.aspx");
        Intent intent;
        intent = new Intent(LoginActivity.this, WebViewActivity.class);
        intent.putExtra("url", url);
        startActivity(intent);
        finish();
    }

 /*   private void initView() {
        btnLogin = (Button)findViewById(R.id.btn_login);
        tvForgertPwd = (TextView) findViewById(R.id.tv_forgetPwd);
        tvRegister = (TextView) findViewById(R.id.tv_register);
        // 设置按钮监听器
        btnLogin.setOnClickListener(this);
        tvForgertPwd.setOnClickListener(this);
        tvRegister.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()){
            case R.id.btn_login :
                CheckUser();
                break;
        }
    }

    *//**
     * 检测输入用户名正确性
     *//*
    private void CheckUser() {
        Intent intent = new Intent(this,MainActivity.class);
        startActivity(intent);
        finish();
    }*/
}
