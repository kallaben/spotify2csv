# Copyright (c) HashiCorp, Inc.
# SPDX-License-Identifier: MPL-2.0

variable "project_id" {
  description = "project id"
}

variable "region" {
  description = "region"
}

provider "google" {
  project = var.project_id
  region  = var.region
}

# VPC
resource "google_compute_network" "vpc" {
  name                    = "${var.project_id}-vpc"
  auto_create_subnetworks = "false"
}

# Subnet
resource "google_compute_subnetwork" "subnet" {
  name          = "${var.project_id}-subnet"
  region        = var.region
  network       = google_compute_network.vpc.name
  ip_cidr_range = "10.5.0.0/20"

  secondary_ip_range {
    range_name    = "services-range"
    ip_cidr_range = "10.0.0.0/14"
  }

  secondary_ip_range {
    range_name    = "pod-ranges"
    ip_cidr_range = "10.4.0.0/19"
  }
}