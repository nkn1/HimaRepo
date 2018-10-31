# **HimaRepo**

## **アプリの概要**
妻を「ニヤッ」と笑かすことだけが目的の **LINE BOT** です。  

ウチの子（ひまり）のおバカな言動を夫婦間で報告し合うとき、映画『マイノリティーレポート』をもじって「ひまのりてぃれぽぉと」と銘打っていました。  
これが【 HimaRepo 】という名前の由来です。  

## **機能**
1. お返事機能  
LINEの会話内に特定のキーワードが含まれていたら、予め設定した返事をします。
1. 表情判定機能  
投稿写真に移っている表情に応じて返事をします。
1. 占い機能  
毎朝、私たち夫婦の星座占いをLINEへPUSH通知します。

## **利用する外部サービス**
1. LINE Messaging API
1. Microsoft Azure Functions
1. Microsoft Azure Face API
1. Web ad Fortune 無料版API　powerd by <a href="http://jugemkey.jp/api/">JugemKey</a> 【PR】<a href="http://www.tarim.co.jp/">原宿占い館 塔里木</a>  

## **当アプリの設定項目**
### **回答パターンファイル**
お返事機能と表情判定機能で使用する「**キーワード**と**回答**の組み合わせ」を指定するJson書式のファイルです。  
デフォルトのファイル名は「AnswerPatterns.json」で、hima_repo.csproj にてデプロイ対象としています。  
回答パターンファイルのサンプルは下記のとおりです。
~~~
{
    "AnswerPattern":[
        {
            "keyword":"こんにちは",
            "answer":"はい、こんにちは"
        },
        {
            "keyword":"ばいばい",
            "answer":"またねー"
        }
    ]
}
~~~


### **Azure Functions のアプリケーション設定**
このアプリは、Azure 関数アプリとして動作し、機密情報等は「アプリケーション設定」から読み込みます。  
必要な設定項目は下表の通りです。  

**アプリ設定名**           | **設定内容の説明**
--------------------------|--------------------
ANSWER_PATTERN_FILE       | 回答パターンファイルのフルPATH
AZURE_FACE_API_KEY        | Microsoft Azure Face API呼出し時に指定するサブスクリプションKEY
LINE_CHANNEL_ACCESS_TOKEN | LINE Messaging API呼び出し時に指定するアクセストークン
LINE_PUSH_TO              | 占いのPUSH通知先となるID（LINE IDとは別物です）
