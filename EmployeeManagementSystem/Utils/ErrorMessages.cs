using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Utils
{
    public static class ErrorMessages
    {
        public const string ERR001_EMPTY_FIELD = "空白の項目があります。";

        public const string ERR002_KANA_FIRST_NAME_LIMIT  = "姓（かな）は25文字以内で入力してください。";

        public const string ERR003_INPUT_HIRAGANA_ONLY = "平仮名のみ入力してください。";

        public const string ERR004_MISSING_KANA_FIRST_NAME = "姓（かな）は入力必須です。";

        public const string ERR005_KANA_LAST_NAME_LIMIT = "名（かな）は25文字以内で入力してください。";

        public const string ERR006_MISSING_KANA_LAST_NAME = "名（かな）は入力必須です。";

        public const string ERR007_FIRST_NAME_LIMIT = "姓 は25文字以内で入力してください。";

        public const string ERR008_MISSING_FIRST_NAME = "姓 は入力必須です。";

        public const string ERR009_LAST_NAME_LIMIT = "名 は25文字以内で入力してください。";

        public const string ERR010_MISSING_LAST_NAME = "名 は入力必須です。";

        public const string ERR011_MISSING_MAIL = "メールアドレス は入力必須です。";

        public const string ERR012_REQUIRED_VALID_MAIL = "有効なメールアドレスを入力してください。";

        public const string ERR013_MISSING_PHONE_NUM = "電話番号 は入力必須です。";

        public const string ERR014_REQUIRED_VALID_PHONE_NUM = "電話番号は「080-1234-5678」の形式で入力してください。";

        public const string ERR015_MISSING_OFFICE = "拠点 は入力必須です。";

        public const string ERR016_LOCATION_NAME_INCORRECT = "拠点名が間違っています。";

        public const string ERR017_MISSING_POSITION = "役職 は入力必須です。";

        public const string ERR018_POSITION_NAME_INCORRECT = "役職名が間違っています。";

        public const string ERR019_DATABASE_READ_ERROR = "データの読み込み中にエラーが発生しました:";

        public const string ERR020_ERROR_DURING_SEARCH = "検索中にエラーが発生しました:";

        public const string ERR021_DATE_MUST_BE_PAST_OR_PRESENT = "日付は現在以前である必要があります。";

        public const string ERR022_MISSING_PASSWORD_AND_MAIL = "メールアドレスとパスワードを入力してください。";

        public const string ERR022_REQUIRED_VALID_PASSWORD_AND_MAIL = "メールアドレスまたはパスワードが正しくありません。";

        public const string ERR024_REQUIRED_VALID_EMPLOYEE_IDS = "検索エリアの社員番号に、[数字][半角スペース][カンマ( , )]以外入力しないでください";

        public const string ERR025_OVERFLOW_NUMBER = "範囲を超える値が入力されています。正しい社員番号を入力してください。";

        public const string ERR026_HAVING_INPUT_ERROR = "入力エラーがあります。修正してください。";

        public const string ERR027_STATUS_UPDATE_ERROR = "ステータス更新中にエラーが発生しました:";

        public const string ERR028_DELETE_FAILED = "削除が失敗しました。";

        public const string ERR029_NOT_FIND_EMPLOYEE = "該当する社員のデータが見つかりません。";

        public const string ERR030_DATA_SAVE_ERROR = "データの保存中にエラーが発生しました:";

        public const string ERR031_DATA_UPDATE_ERROR = "更新中にエラーが発生しました:";

        public const string ERR032_COULD_NOT_UPDATE = "データの更新ができませんでした。\n入力した内容をご確認ください。";

        public const string ERR033_ERROR = "エラーが発生しました。";


    }
}
