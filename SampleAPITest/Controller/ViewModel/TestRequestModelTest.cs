using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleAPI.Controllers.ViewModel;
using Xunit;

namespace SampleAPITest.Controller.ViewModel
{
    public class TestRequestModelTest
    {
        #region 正常_TheoryData
        //MemberDataで利用するデータにTheoryDataを利用
        //メリット
        //1.記述が短い
        //2.型のチェックが実施される
        [Theory(DisplayName = "正常_TheoryData")]
        [MemberData(nameof(GetOkModel_TheoryData))]
        public void 正常_TheoryData(TestRequestModel model)
        {
            var resultList = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, new ValidationContext(model), resultList, true);

            Assert.True(result);
        }

        public static TheoryData<TestRequestModel> GetOkModel_TheoryData = new TheoryData<TestRequestModel>()
        {
            //AccessID:1～20桁 Length:1～2桁 数字のみ
            new TestRequestModel{ AccessID = "1", Password = "a", Length = 1},
            new TestRequestModel{ AccessID = "12345678901234567890", Password = "12345678901234567890", Length = 9},
            new TestRequestModel{ AccessID = "aaaaaaaaaaaaaaaaaaaa", Password = "aaaaaaaaaaaaaaaaaaaa", Length = 10},
            new TestRequestModel{ AccessID = "AAAAAAAAAAAAAAAAAAAA", Password = "AAAAAAAAAAAAAAAAAAAA", Length = 19},
            new TestRequestModel{ AccessID = "@@@@@@@@@@@@@@@@@@@@", Password = "@@@@@@@@@@@@@@@@@@@@", Length = 20},
            new TestRequestModel{ AccessID = "12345aaaaaAAAAA@@@@@", Password = "12345aaaaaAAAAA@@@@@", Length = 29},
            new TestRequestModel{ AccessID = "12345aaaaaAAAAA@@@@@", Password = "12345aaaaaAAAAA@@@@@", Length = 30},
        };
        #endregion

        #region 正常_IEnumerable
        //MemberDataで利用するデータにIEnumerable<object[]>を利用
        //object型を利用しているため型チェックがされない
        //基本的にTheoryDataを利用したほうがよい
        [Theory(DisplayName = "正常_IEnumerable")]
        [MemberData(nameof(GetOkModel_IEnumerable))]
        public void 正常_IEnumerable(TestRequestModel model)
        {
            var resultList = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, new ValidationContext(model), resultList, true);

            Assert.True(result);
        }

        public static IEnumerable<object[]> GetOkModel_IEnumerable => new List<object[]>() {

            //AccessID:1～20桁 Length:1～30 数字のみ
            new object[]{new TestRequestModel {
                AccessID = "1",
                Password = "a",
                Length = 1,
            }},
            new object[]{new TestRequestModel {
                AccessID = "12345678901234567890",
                Password = "12345678901234567890",
                Length = 9,
            }},
            new object[]{new TestRequestModel {
                AccessID = "aaaaaaaaaaaaaaaaaaaa",
                Password = "aaaaaaaaaaaaaaaaaaaa",
                Length = 10,
            }},
            new object[]{new TestRequestModel {
                AccessID = "AAAAAAAAAAAAAAAAAAAA",
                Password = "AAAAAAAAAAAAAAAAAAAA",
                Length = 19,
            }},
            new object[]{new TestRequestModel {
                AccessID = "@@@@@@@@@@@@@@@@@@@@",
                Password = "@@@@@@@@@@@@@@@@@@@@",
                Length = 20,
            }},
            new object[]{new TestRequestModel {
                AccessID = "12345aaaaaAAAAA@@@@@",
                Password = "12345aaaaaAAAAA@@@@@",
                Length = 29,
            }},
            new object[]{new TestRequestModel {
                AccessID = "12345aaaaaAAAAA@@@@@",
                Password = "12345aaaaaAAAAA@@@@@",
                Length = 30,
            }},
        };
        #endregion

        #region 正常_ClassData
        //ClassDataを利用
        //テストデータ作成ロジックを複数テストクラスで使いまわす場合に利用
        [Theory(DisplayName = "正常_ClassData")]
        [ClassData(typeof(GetTestRequestModel))]
        public void 正常_ClassData(TestRequestModel model)
        {
            var resultList = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, new ValidationContext(model), resultList, true);

            Assert.True(result);
        }

        public class GetTestRequestModel : TheoryData<TestRequestModel>
        {
            public GetTestRequestModel()
            {
                //AccessID:1～20桁 Length:1～2桁 数字のみ
                Add(new TestRequestModel{ AccessID = "1", Password = "a", Length = 1});
                Add(new TestRequestModel{ AccessID = "12345678901234567890", Password = "12345678901234567890", Length = 9});
                Add(new TestRequestModel{ AccessID = "aaaaaaaaaaaaaaaaaaaa", Password = "aaaaaaaaaaaaaaaaaaaa", Length = 10});
                Add(new TestRequestModel{ AccessID = "AAAAAAAAAAAAAAAAAAAA", Password = "AAAAAAAAAAAAAAAAAAAA", Length = 19});
                Add(new TestRequestModel{ AccessID = "@@@@@@@@@@@@@@@@@@@@", Password = "@@@@@@@@@@@@@@@@@@@@", Length = 20});
                Add(new TestRequestModel{ AccessID = "12345aaaaaAAAAA@@@@@", Password = "12345aaaaaAAAAA@@@@@", Length = 29});
                Add(new TestRequestModel{ AccessID = "12345aaaaaAAAAA@@@@@", Password = "12345aaaaaAAAAA@@@@@", Length = 30});
            }
        }
        #endregion

        #region 異常
        [Theory(DisplayName = "異常")]
        [MemberData(nameof(GetNGModel))]
        public void 異常(TestRequestModel model)
        {
            var resultList = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, new ValidationContext(model), resultList, true);

            Assert.False(result);
        }

        public static TheoryData<TestRequestModel> GetNGModel = new TheoryData<TestRequestModel>()
        {
            //AccessID:21桁
            new TestRequestModel{ AccessID = "123456789012345678901", Password = "a", Length = 1},
            //AccessID:空
            new TestRequestModel{ AccessID = "", Password = "a", Length = 1},
            //Password:21桁
            new TestRequestModel{ AccessID = "12345678901234567890", Password = "123456789012345678901", Length = 1},
            //Password:21桁
            new TestRequestModel{ AccessID = "12345678901234567890", Password = "", Length = 1},
            //Length:31
            new TestRequestModel{ AccessID = "12345678901234567890", Password = "a", Length = 31},
            //Length:0
            new TestRequestModel{ AccessID = "12345678901234567890", Password = "a", Length = 0},
            //AccessIDなし
            new TestRequestModel{Password = "a", Length = 0},
            //AccessIDなし
            new TestRequestModel{AccessID = "12345678901234567890", Length = 0},
            //Lengthなし
            new TestRequestModel{ AccessID = "12345678901234567890", Password = "a"},
        };
        #endregion
    }
}
