using Microsoft.AspNetCore.Mvc;

public class ErrorHandlingController : ControllerBase
{
    [HttpGet("divide")]
    public IActionResult GetDivisionResult(int numerator, int denominator)
    {
        try
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }

            var result = numerator / denominator;
            return Ok(new { Result = result });
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
            return BadRequest("Division by zero is not allowed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}