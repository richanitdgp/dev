import asyncio

# Declaring main as async 
# invoking it returns a coroutine
# need to await on coroutine to kickstart its execution
async def main():
    print("Tim")

    # run foo as an async task so other things can run when its sleeping
    task = asyncio.create_task(foo('some text'))
    print('finished')

# another async function
# await on asyncio.sleep since its a coroutine
async def foo(text):
    print(text)

    # this function sleeps for 1s 
    # we can run something else during this sleep time
    await asyncio.sleep(1)

# When we create async function in Python, 
# we need to start an event loop
# asyncio.run adds the coroutine to the event loop
asyncio.run(main())