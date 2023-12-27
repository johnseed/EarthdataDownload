# EarthdataDownload

## Search

海流
```bash
curl 'https://cmr.earthdata.nasa.gov/search/granules.json' \
  -H 'authority: cmr.earthdata.nasa.gov' \
  -H 'accept: application/json, text/plain, */*' \
  -H 'accept-language: zh-CN,zh;q=0.9' \
  -H 'client-id: eed-edsc-prod-serverless-client' \
  -H 'cmr-request-id: 91ca47a2-c396-4478-8652-8e90a7b65c01' \
  -H 'content-type: application/x-www-form-urlencoded' \
  -H 'origin: https://search.earthdata.nasa.gov' \
  -H 'referer: https://search.earthdata.nasa.gov/' \
  -H 'sec-ch-ua: "Microsoft Edge";v="119", "Chromium";v="119", "Not?A_Brand";v="24"' \
  -H 'sec-ch-ua-mobile: ?0' \
  -H 'sec-ch-ua-platform: "Windows"' \
  -H 'sec-fetch-dest: empty' \
  -H 'sec-fetch-mode: cors' \
  -H 'sec-fetch-site: same-site' \
  -H 'user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0' \
  --data-raw 'echo_collection_id=C2102958977-POCLOUD&page_num=1&page_size=20&temporal=2023-12-21T00:00:00.000Z,2023-12-21T23:59:59.999Z&sort_key=-start_date'
```

海面风场
```bash
curl 'https://cmr.earthdata.nasa.gov/search/granules.json' \
  -H 'authority: cmr.earthdata.nasa.gov' \
  -H 'accept: application/json, text/plain, */*' \
  -H 'accept-language: zh-CN,zh;q=0.9' \
  -H 'client-id: eed-edsc-prod-serverless-client' \
  -H 'cmr-request-id: b66f8bdd-2588-48ca-bbae-4710ce072592' \
  -H 'content-type: application/x-www-form-urlencoded' \
  -H 'origin: https://search.earthdata.nasa.gov' \
  -H 'referer: https://search.earthdata.nasa.gov/' \
  -H 'sec-ch-ua: "Microsoft Edge";v="119", "Chromium";v="119", "Not?A_Brand";v="24"' \
  -H 'sec-ch-ua-mobile: ?0' \
  -H 'sec-ch-ua-platform: "Windows"' \
  -H 'sec-fetch-dest: empty' \
  -H 'sec-fetch-mode: cors' \
  -H 'sec-fetch-site: same-site' \
  -H 'user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0' \
  --data-raw 'echo_collection_id=C2075141638-POCLOUD&page_num=1&page_size=20&temporal=2022-12-31T20:00:02.000Z,2022-12-31T21:59:59.000Z&sort_key=-start_date' 
```
