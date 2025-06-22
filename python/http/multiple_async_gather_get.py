import http.client
import asyncio
import time
import aiohttp

# Need to install aiohttp before runnin this
# pip install aiohttp

# fetch_all to handle tasks for all urls 
async def fetch_all(urls):
    # Use this single client session for multiple urls
    async with aiohttp.ClientSession() as session:
        for url in urls:
            tasks = [session.get(url) for url in urls]
            responses = await asyncio.gather(*tasks)
            for resp in responses:
                print(await resp.text())

async def main():
    urls = [
        "https://microsoft.com",
        "https://apple.com",
        "https://google.com",
        "https://facebook.com"
    ]

    start_time = time.time()
    await fetch_all(urls)
    elapsed_time = time.time() - start_time

    print(f"\n✅ Total time taken: {elapsed_time:.2f} seconds")
    

asyncio.run(main())