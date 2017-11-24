using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Google.Protobuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sdk.dto;
using sdk.sdkattribute;
using sdk.rpc;
using System.Threading;
using System.Net;
using sdk.crypto;
using sdk.sdkenum;

namespace ConsoleApp2
{
    
    class Program
    {

       
        public static void testQueryBalance()
        {

            string testUrl = "http://bc-test.ipaynow.cn/gateway";
            string testKey = "013f81ac3ee1101b620031c00eac22ab53334c083c09fc191e05c29c9f0d26ad";
            string testMerchant = "000100000000010000000000000001";
            IpayNowClient ipayNowClient = new DefaultIpayNowClient(testUrl, testKey, testMerchant);

            QueryReqDto reqDto = new QueryReqDto();
            reqDto.MhtOrderNo = "131448318439473166";
            reqDto.AppId = "1459846530407363";//
       //     reqDto.Router = "00010000000003";//
                                               
            Console.WriteLine(ipayNowClient.BalanceQuery(reqDto));
        }

        public static void testQuery()
        {
            string testUrl = "http://bc-test.ipaynow.cn/gateway";
            string testKey = "013f81ac3ee1101b620031c00eac22ab53334c083c09fc191e05c29c9f0d26ad";
            string testMerchant = "000100000000010000000000000001";
            IpayNowClient ipayNowClient = new DefaultIpayNowClient(testUrl, testKey, testMerchant);

            QueryReqDto reqDto = new QueryReqDto();
            reqDto.MhtOrderNo = "131448318439473166";
            reqDto.AppId = "1459846530407363";
            TransQueryRespDto dto = ipayNowClient.TransQuery(reqDto);
            Console.WriteLine(dto);
        }

        public static void testPay()
        {
            string testUrl = "http://bc-test.ipaynow.cn/gateway";
            string testKey = "013f81ac3ee1101b620031c00eac22ab53334c083c09fc191e05c29c9f0d26ad";
            string testMerchant = "000100000000010000000000000001";
            IpayNowClient ipayNowClient = new DefaultIpayNowClient(testUrl, testKey, testMerchant);

            AgentPayReqDto agentPayReqDto = new AgentPayReqDto();
            agentPayReqDto.MhtOrderAmt = 1;
        
            String orderid = DateTime.Now.ToFileTimeUtc().ToString();
            Console.WriteLine(orderid);
            agentPayReqDto.MhtOrderNo = "131449335883450759";
            agentPayReqDto.AppId = "1459846530407363";
            agentPayReqDto.AgentPayMemo = "test";
            agentPayReqDto.MhtReqTime = "20170307100312";
            agentPayReqDto.AccType = "0";
            agentPayReqDto.PayeeName = "袁海杰";
            agentPayReqDto.PayeeCardNo = "6214830113071483";
            agentPayReqDto.PayeeCardUnionNo = "";
            agentPayReqDto.NotifyUrl = "http://mock-api.com/WmnE6LKJ.mock/tongzhi1";

            long startTime = DateTime.Now.ToFileTimeUtc();
            TransRespDto dto = ipayNowClient.AgentPay(agentPayReqDto);
            Console.WriteLine(dto);
            long endTime = DateTime.Now.ToFileTimeUtc();
            Console.WriteLine(String.Format("startTime:{0}, endTime:{1}, span:{2}", startTime, endTime, endTime - startTime));
        }


        public static void testVerify() {
            string data = "{\"data\":{\"responseCode\":\"A001\",\"transType\":\"AGENT_PAY\",\"chTransId\":\"200003201706281611301869886\",\"mhtOrderNo\":\"1498637485119\",\"responseMsg\":\"成功\",\"transStatus\":\"SUCCESSED\",\"responseTime\":\"20170628161137\",\"mhtOrderAmt\":1},\"sign_v\":27,\"sign_r\":\"b1e545850890a10f14b820675dc0258839b72c05fe562ff99b858f5741ff9a29\",\"sign_s\":\"5be45874b4bfa474835f400c4fcba05e5026e1d790513cbd1f40cbe0ad4642f4\"}";
            bool vr = ECKey.Verify(data, "048a6fc962b5d40ae3253d3231f7742c66334cc6eb516ac3499c00358964e3421569058bccd4437c55ca391559fea98f2b78a20400d6df39f0ef4715ce5dcdbd57");
            Console.WriteLine(vr);
        }

        public static void testAgentPayRefundBatchQuery() {

            string testUrl = "http://bc-test.ipaynow.cn/gateway";
            string testKey = "013f81ac3ee1101b620031c00eac22ab53334c083c09fc191e05c29c9f0d26ad";
            string testMerchant = "000100000000010000000000000001";
            IpayNowClient ipayNowClient = new DefaultIpayNowClient(testUrl, testKey, testMerchant);

            BatchQueryReqDto batchQueryReqDto = new BatchQueryReqDto();
            batchQueryReqDto.AppId = "1459846530407363";
            batchQueryReqDto.MhtOrderNo = "123456";
            batchQueryReqDto.RefundDate = "20170414";
            batchQueryReqDto.NowPage = 2;
            batchQueryReqDto.PageSize = 2;

            AgentPayRefundBatchQueryRespDto dto = ipayNowClient.agentPayRefundBatchQuery(batchQueryReqDto);
            Console.WriteLine(dto);
        }

        public static void testAgentPayRefundQuery() {
            string testUrl = "http://bc-test.ipaynow.cn/gateway";
            string testKey = "013f81ac3ee1101b620031c00eac22ab53334c083c09fc191e05c29c9f0d26ad";
            string testMerchant = "000100000000010000000000000001";
            IpayNowClient ipayNowClient = new DefaultIpayNowClient(testUrl, testKey, testMerchant);
            QueryReqDto reqDto = new BatchQueryReqDto();
            reqDto.AppId = "1459846530407363";
            reqDto.MhtOrderNo = "gzh201704141359106kYQIF2aAvyBwIzw";
            AgentPayRefundQueryRespDto dto = ipayNowClient.agentPayRefundQuery(reqDto);
            Console.WriteLine(dto);

        }

        static void Main(string[] args)
        {
            //   testPay();
            //  testQuery();
            //  testQueryBalance();
              testAgentPayRefundBatchQuery();
            //  testAgentPayRefundQuery();
          
            Console.ReadKey(true);
        }
    }
}
