﻿using System;

namespace QPlay.Identity.Contracts;

public record DebitGil(Guid UserId, decimal Gil, Guid CorrelationId);