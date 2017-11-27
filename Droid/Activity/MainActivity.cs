﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase;
using Firebase.Auth;
using System;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Beyond.Droid.Activity;

namespace Beyond.Droid
{
    [Activity(Label = "Beyond", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        Button btnLogin;
        EditText input_email, input_password;
        TextView btnSignUp, btnForgotPassword;

        RelativeLayout activity_main;

        public static FirebaseApp app;
        FirebaseAuth auth;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //Init Firebase
            InitFirebaseAuth();

            //View
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            btnForgotPassword = FindViewById<TextView>(Resource.Id.login_btn_forgot_password);
            activity_main = FindViewById<RelativeLayout>(Resource.Id.activity_main);

            btnSignUp.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
            btnForgotPassword.SetOnClickListener(this);
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder().SetApplicationId("1:1003957109154:android:ab5dee3d119f3c76").SetApiKey("AIzaSyDAtzCB1fgkr4CySy1IVszRcbnS9SUKW5U").Build();


            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            auth = FirebaseAuth.GetInstance(app);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.login_btn_forgot_password)
            {
                StartActivity(new Android.Content.Intent(this, typeof(ForgotPassword)));
                Finish();
            }
            else if (v.Id == Resource.Id.login_btn_sign_up)
            {
                StartActivity(new Android.Content.Intent(this, typeof(SignUp)));
                Finish();
            }
            else if (v.Id == Resource.Id.login_btn_login)
            {
                LoginUser(input_email.Text, input_password.Text);
            }
        }

        private void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, typeof(DashBoard)));
                Finish();
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(activity_main, "Login Failed ", Snackbar.LengthShort);
                snackBar.Show();
            }
        }
    }
}

