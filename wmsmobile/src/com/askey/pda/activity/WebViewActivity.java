package com.askey.pda.activity;

import com.askey.wms.R;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;


public class WebViewActivity extends Activity {
    private static final String INDEX_URl = "http://121.41.45.122/PDA/WelcomePDA.aspx";

    private WebView mWebView;
    private String mUrl;
    private WebSettings settings;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_web_view);

        mUrl = getIntent().getStringExtra("url");

        mWebView = (WebView)findViewById(R.id.wv_news_detail);

        mWebView.loadUrl(mUrl);   //加载该网页

        //有关于WebView的所有设置，都需要先获取到setting
        settings = mWebView.getSettings();
        settings.setBuiltInZoomControls(true); //显示缩放按钮 （wap网页不支持）
        settings.setUseWideViewPort(true);  //支持双击缩放 （wap网页不支持）
        settings.setJavaScriptEnabled(true); // 支持js功能


        //显示进度条 && 在跳转链接时强制在当前webview中加载
        mWebView.setWebViewClient(new WebViewClient(){
            @Override
            public void onPageStarted(WebView view, String url, Bitmap favicon) {
                super.onPageStarted(view, url, favicon);
//                pbLoading.setVisibility(View.VISIBLE);
            }

            @Override
            public void onPageFinished(WebView view, String url) {
                super.onPageFinished(view, url);
//                pbLoading.setVisibility(View.INVISIBLE);
            }

            // 所有链接跳转会走此方法
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
              System.out.println("跳转链接:" + url);
                if(INDEX_URl.equals(url)){
                    Intent intent = null;// 创建Intent对象
                    intent = new Intent(WebViewActivity.this,MainActivity.class);// 窗口初始化Intent
                    startActivity(intent);
                    finish();
                }else {
                    view.loadUrl(url);// 在跳转链接时强制在当前webview中加载
                }

                return true;
            }
        });

       /* mWebView.goBack();//跳到上个页面*/
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        mWebView.clearFormData();
        mWebView.clearHistory();
        mWebView.destroy();
    }
}
